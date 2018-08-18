using RandomGen;
using System.Collections.Generic;

namespace Ikkiuchi.Core {

    //  フェーズ
    public enum Phase {
        TurnStart,  //  ターン開始
        MovePlot,   //  移動プロット
        ActionPlot, //  行動プロット
        Resolve,    //  解決
        TurnEnd,    //  ターン終了
        Settle, //  決着
    }

    public class Controller {

        private IRule rule; //  TODO:   inject

        private IPlayer player1;
        private IPlayer player2;

        private ICardSet cardSet;
        private IDeck deck;
        private ITrash trash;

        private Phase currentPhase;
        private bool isP1MovePlotted;
        private bool isP2MovePlotted;
        private bool isP1ActionPlotted;
        private bool isP2ActionPlotted;

        private RandomGenerator randGen;   //  TODO:   inject

        //  ボードを作成する
        public void MakeBoard() {

            player1 = new Player(true, rule);
            player2 = new Player(false, rule);

            cardSet = new CardSet.Factory().CreateClassic();
            deck = new Deck();
            trash = new Trash();

            deck.AppendCards(cardSet.EnumerateNormalCards().Shuffle(randGen));
            deck.AppendTrumps(cardSet.EnumerateTrumps().Shuffle(randGen));

            currentPhase = Phase.TurnStart;
        }

        //  カードを配る
        public void DealCards() {
            int handMax = rule.CountOfMoment * 2;

            //  切り札をドロー
            if (rule.IsEnableTrump) {
                player1.DrawTrump(deck);
                player2.DrawTrump(deck);
            }

            //  両プレイヤーの手札がMaxになるまで交互にドローする
            IPlayer targetPlayer = player1;
            while (player1.Hand.Cards.Count < handMax || player2.Hand.Cards.Count < handMax) {

                if (deck.IsEmpty()) {   //  デッキが空になったらtrashから全部取り出してシャッフルしてデッキにする
                    deck.AppendCards(trash.Remove().Shuffle(randGen));
                }
                targetPlayer.Hand.Cards.Add(deck.Deal());

                targetPlayer = targetPlayer == player1 ? player2 : player1;
            }

            isP1MovePlotted = false;
            isP2MovePlotted = false;
            isP1ActionPlotted = false;
            isP2ActionPlotted = false;

            currentPhase = Phase.MovePlot;
        }

        //  移動プロット
        //  プロットしない箇所は-1
        public void MovePlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment; ++i) {
                if (plots[i] == -1) continue;
                player.Plots.PlotMove(i, cardSet.GetCard(plots[i]));
            }

            if(player == player1) {
                isP1MovePlotted = true;
            }
            else {
                isP2MovePlotted = true;
            }

            if(isP1MovePlotted && isP2MovePlotted) {
                currentPhase = Phase.ActionPlot;
            }
        }

        //  行動プロット
        //  プロットしない箇所は-1
        public void ActionPlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment; ++i) {
                if (plots[i] == -1) continue;
                player.Plots.PlotAction(i, cardSet.GetCard(plots[i]));
            }

            if (player == player1) {
                isP1ActionPlotted = true;
            }
            else {
                isP2ActionPlotted = true;
            }

            if (isP1ActionPlotted && isP2ActionPlotted) {
                currentPhase = Phase.Resolve;
            }
        }

        //  解決
        public void Resolve() {
            IList<IAction> p1Actions = player1.GetActualActions(rule.CountOfMoment);
            IList<IAction> p2Actions = player2.GetActualActions(rule.CountOfMoment);

            for(int i = 0; i < rule.CountOfMoment; ++i) {
                IAction p1a = p1Actions[i];
                IAction p2a = p2Actions[i];

                //  優先度が同じとき同時に処理する
                if(p1a.ResolveOrder == p2a.ResolveOrder) {
                    //  移動先がぶつかるかどうかを確認する
                    Pos p1to = player1.Gradiator.RelativePosToAbsolute(p1a.GetMoveTo());
                    Pos p2to = player2.Gradiator.RelativePosToAbsolute(p2a.GetMoveTo());
                    //  移動先が同じまたはp1の移動先がp2の位置かつp2の移動先がp1の位置(交差)ならば衝突としてマーク
                    bool isCollide = p1to == p2to ||
                        (p1to == player2.Gradiator.Position && p2to == player1.Gradiator.Position);

                    p1a.Resolve(isCollide);
                    p2a.Resolve(isCollide);

                    if(player1.Life.IsDead || player2.Life.IsDead) {
                        currentPhase = Phase.Settle;
                        return; //  どちらかが死ねば終了
                    }
                }
                else {
                    IAction first = p1a.ResolveOrder < p2a.ResolveOrder ? p1a : p2a;
                    IAction second = first == p1a ? p2a : p1a;

                    first.Resolve(false);
                    if (player1.Life.IsDead || player2.Life.IsDead) {
                        currentPhase = Phase.Settle;
                        return; //  どちらかが死ねば終了
                    }

                    second.Resolve(false);
                    if (player1.Life.IsDead || player2.Life.IsDead) {
                        currentPhase = Phase.Settle;
                        return; //  どちらかが死ねば終了
                    }

                }
            }
        }

        public void TurnEnd() {
            //  プロット状態を更新
            player1.Plots.OnTurnEnd(trash);
            player2.Plots.OnTurnEnd(trash);

            //  切り札を除く手札を捨てる
            player1.Hand.TrashExcludeTrump(trash);
            player2.Hand.TrashExcludeTrump(trash);

            currentPhase = Phase.TurnStart;
        }
    }
}
