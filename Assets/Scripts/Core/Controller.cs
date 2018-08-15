using RandomGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkkiuchiV2.Core {
    public class Controller {

        private IRule rule; //  TODO:   inject

        private IPlayer player1;
        private IPlayer player2;

        private ICardSet cardSet;
        private IDeck deck;
        private ITrash trash;

        private IRandomGenerator randGen;   //  TODO:   inject

        //  ボードを作成する
        public void MakeBoard() {

            player1 = new Player(true, rule);
            player2 = new Player(false, rule);

            cardSet = new CardSet.Factory().CreateClassic();
            deck = new Deck();
            trash = new Trash();

            deck.AppendCards(cardSet.EnumerateNormalCards().Shuffle(randGen));
            deck.AppendTrumps(cardSet.EnumerateTrumps().Shuffle(randGen));
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
        }

        //  移動プロット
        public void MovePlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment; ++i) {
                player.Plots.PlotMove(i, cardSet.GetCard(plots[i]));
            }
        }

        //  行動プロット
        //  プロットしない箇所は-1
        public void ActionPlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment; ++i) {
                if (plots[i] == -1) continue;
                player.Plots.PlotAction(i, cardSet.GetCard(plots[i]));
            }
        }

        //  解決
        public void Resolve() {

        }
    }
}
