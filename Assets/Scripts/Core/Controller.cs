using RandomGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Ikkiuchi.Core {

    //  フェーズ
    public enum Phase {
        None,
        Start,
        MovePlot,   //  移動プロット
        ActionPlot, //  行動プロット
        Resolve,    //  解決
        Settle, //  決着
    }

    public class Controller {

        [Inject] private IRule rule;

        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }

        private ICardSet cardSet;
        private IDeck deck;
        private ITrash trash;

        public Phase CurrentPhase { get; set; } = Phase.None;
        private bool isP1MovePlotted;
        private bool isP2MovePlotted;
        private bool isP1ActionPlotted;
        private bool isP2ActionPlotted;

        public IPlayer MyPlayer { get; set; }

        [Inject] private RandomGenerator randGen;

        public bool IsReadyMove(IPlayer player) {
            return player == Player1 ? isP1MovePlotted : isP2MovePlotted;
        }

        public bool IsReadyAction(IPlayer player) {
            return player == Player1 ? isP1ActionPlotted : isP2ActionPlotted;
        }

        public void SetSeed(uint seed) {
            randGen.Seed = seed;
        }

        //  ボードを作成する
        public void MakeBoard(bool isPlayer1) {
            CurrentPhase = Phase.Start;

            Player1 = new Player(true, rule);
            Player2 = new Player(false, rule);

            cardSet = new CardSet.Factory(this).CreateClassic();
            deck = new Deck();
            trash = new Trash();

            deck.AppendCards(cardSet.EnumerateNormalCards().Shuffle(randGen));
            deck.AppendTrumps(cardSet.EnumerateTrumps().Shuffle(randGen));

            MyPlayer = isPlayer1 ? Player1 : Player2;
        }

        public IPlayer EnemyPlayer {
            get {
                return MyPlayer == Player1 ? Player2 : Player1;
            }
        }


        //  カードを配る
        public void DealCards() {
            int handMax = rule.CountOfMoment.Value * 2;

            //  切り札をドロー
            if (rule.IsEnableTrump) {
                Player1.DrawTrump(deck);
                Player2.DrawTrump(deck);
            }

            //  両プレイヤーの手札がMaxになるまで交互にドローする
            IPlayer targetPlayer = Player1;
            while (Player1.Hand.Cards.Count < handMax || Player2.Hand.Cards.Count < handMax) {

                if (deck.IsEmpty()) {   //  デッキが空になったらtrashから全部取り出してシャッフルしてデッキにする
                    deck.AppendCards(trash.Remove().Shuffle(randGen));
                }
                targetPlayer.Hand.Cards.Add(deck.Deal());

                targetPlayer = targetPlayer == Player1 ? Player2 : Player1;
            }

            isP1MovePlotted = false;
            isP2MovePlotted = false;
            isP1ActionPlotted = false;
            isP2ActionPlotted = false;

            CurrentPhase = Phase.MovePlot;
        }

        //  移動プロット
        //  プロットしない箇所は-1
        public void MovePlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                if (plots[i] == -1) continue;
                player.Plots.PlotMove(i, cardSet.GetCard(plots[i]));
            }

            player.Hand.RemoveCards(plots.Where(id => id != -1));

            if(player == Player1) {
                isP1MovePlotted = true;
            }
            else {
                isP2MovePlotted = true;
            }

            if(isP1MovePlotted && isP2MovePlotted) {
                CurrentPhase = Phase.ActionPlot;
            }
        }

        //  行動プロット
        //  プロットしない箇所は-1
        public void ActionPlot(IPlayer player, int[] plots) {
            for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                if (plots[i] == -1) continue;
                player.Plots.PlotAction(i, cardSet.GetCard(plots[i]));
            }

            player.Hand.RemoveCards(plots.Where(id => id != -1));

            if (player == Player1) {
                isP1ActionPlotted = true;
            }
            else {
                isP2ActionPlotted = true;
            }

            if (isP1ActionPlotted && isP2ActionPlotted) {
                CurrentPhase = Phase.Resolve;
            }
        }

        private IEnumerator ResolveRapidAttack(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasRapidAttack()) {
                if(p1a.ResolveRapidAttack(momentIndex,Player1) == 0) {
                    //  miss
                }
                else {
                    // damage
                }
            }

            if (p2a.HasRapidAttack()) {
                if(p2a.ResolveRapidAttack(momentIndex,Player2) == 0) {
                    //  miss
                }
                else {
                    //  damage
                }
            }
            yield return null;
            //  モデル更新
            Player1.Life.DealDamage(p2a.ResolveRapidAttack(momentIndex,Player2));
            Player2.Life.DealDamage(p1a.ResolveRapidAttack(momentIndex,Player1));

            yield return null;
        }

        private IEnumerator ResolveCounter(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasCounter()) {

            }
            if (p2a.HasCounter()) {

            }
            yield return null;
            p1a.ResolveCounter(momentIndex,Player1);
            p2a.ResolveCounter(momentIndex,Player2);

            yield return null;
        }



        private IEnumerator ResolveMove(Pos p1to, Pos p2to) {

            bool p1Move = p1to != Player1.Gradiator.Position;
            bool p2Move = p2to != Player2.Gradiator.Position;
            if (p1Move && p2Move) {
                //  両方移動するとき
                //  移動先が同じまたはp1の移動先がp2の位置かつp2の移動先がp1の位置(交差)ならば衝突としてマーク
                bool isCollide = p1to == p2to ||
                    (p1to == Player2.Gradiator.Position && p2to == Player1.Gradiator.Position);

                if (!isCollide) {
                    //  アニメーション
                    if (p1to.IsInboundBoard()) {

                    }
                    else {
                        //  画面外移動
                    }

                    if (p2to.IsInboundBoard()) {

                    }
                    else {
                        //  画面外移動
                    }
                }
                else {
                    //  衝突しないときのアニメーション
                }
                //  アニメーション実行
                yield return null;
                //  モデル更新
                if (!isCollide) {
                    //  衝突しないとき
                    if (!p1to.IsInboundBoard()) {
                        Player1.Life.DealDamage(1);
                    }
                    else {
                        Player1.Gradiator.Position = p1to;
                    }
                    //  衝突しないとき
                    if (!p2to.IsInboundBoard()) {
                        Player2.Life.DealDamage(1);
                    }
                    else {
                        Player2.Gradiator.Position = p2to;
                    }
                }
            }
            else if (p1Move) {
                //  p1のみ移動するとき
                if (p1to == p2to) {  //  P2を押し込む場合
                    RelativePos relative = new RelativePos(p2to.X - p1to.X, p2to.Y - p1to.Y);
                    if (new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  アニメーション
                        yield return null;
                        //  モデル更新
                        Player1.Gradiator.Position = p1to;
                        Player2.Gradiator.Position = new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  アニメーション
                        yield return null;
                        //  モデル更新
                        Player2.Life.DealDamage(1);
                    }

                }
                else {  //  通常移動
                    //  アニメーション
                    yield return null;
                    //  モデル更新
                    if (p1to.IsInboundBoard()) {
                        Player1.Gradiator.Position = p1to;
                    }
                    else {
                        Player1.Life.DealDamage(1);
                    }
                }
            }
            else if (p2Move) {
                //  p2のみ移動するとき
                if (p1to == p2to) {  //  P1を押し込む場合
                    RelativePos relative = new RelativePos(p1to.X - p2to.X, p1to.Y - p2to.Y);
                    if (new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  アニメーション
                        yield return null;
                        //  モデル更新
                        Player2.Gradiator.Position = p2to;
                        Player1.Gradiator.Position = new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  アニメーション
                        yield return null;
                        //  モデル更新
                        Player1.Life.DealDamage(1);
                    }

                }
                else {  //  通常移動
                    //  アニメーション
                    yield return null;
                    //  モデル更新
                    if (p1to.IsInboundBoard()) {
                        Player2.Gradiator.Position = p2to;
                    }
                    else {
                        Player2.Life.DealDamage(1);
                    }
                }
            }

            yield return null;
        }


        private IEnumerator ResolveActionAttack(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasActionAttack()) {
                if (p1a.ResolveDamage(momentIndex,Player1) == 0) {
                    //  miss
                }
                else {
                    if (Player2.BigCounter) {

                    }
                    else {
                        // damage

                    }
                    if (Player2.Cotton) {

                    }
                    if (Player2.Counter) {

                    }
                }
            }

            if (p2a.HasActionAttack()) {
                if (p2a.ResolveDamage(momentIndex,Player2) == 0) {
                    //  miss
                }
                else {
                    if (Player1.BigCounter) {

                    }
                    else {
                        // damage

                    }
                    if (Player1.Cotton) {

                    }
                    if (Player1.Counter) {

                    }
                }
            }
            yield return null;
            //  モデル更新
            if (p1a.HasActionAttack()) {

                if (Player2.BigCounter) {
                    Player1.Life.DealDamage(p1a.ResolveDamage(momentIndex,Player1) * 2);
                }
                else {
                    // damage
                    Player2.Life.DealDamage(p1a.ResolveDamage(momentIndex,Player1));
                }
                if (Player2.Cotton) {
                    Player2.Life.DealDamage(-1);
                }
                if (Player2.Counter) {
                    Player1.Life.DealDamage(p1a.ResolveDamage(momentIndex,Player1));
                }
            }

            if (p2a.HasActionAttack()) {
                if (Player1.BigCounter) {
                    Player2.Life.DealDamage(p2a.ResolveDamage(momentIndex,Player2) * 2);
                }
                else {
                    // damage
                    Player1.Life.DealDamage(p2a.ResolveDamage(momentIndex,Player2));
                }
                if (Player1.Cotton) {
                    Player1.Life.DealDamage(-1);
                }
                if (Player1.Counter) {
                    Player2.Life.DealDamage(p2a.ResolveDamage(momentIndex,Player2));
                }
            }

            yield return null;
        }

        private bool IsSettled() {
            return Player1.Life.IsDead || Player2.Life.IsDead;
        }

        //  解決
        public IEnumerator Resolve() {
            while (true) {

                IList<IAction> p1Actions = Player1.GetActualActions(rule.CountOfMoment.Value, this);
                IList<IAction> p2Actions = Player2.GetActualActions(rule.CountOfMoment.Value, this);

                for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                    IAction p1a = p1Actions[i];
                    IAction p2a = p2Actions[i];

                    //  カウンター状態のクリア
                    Player1.Cotton = false;
                    Player1.BigCounter = false;
                    Player1.Counter = false;
                    Player2.Cotton = false;
                    Player2.BigCounter = false;
                    Player2.Counter = false;

                    //  先制移動処理
                    yield return ResolveMove(p1a.ExpectedRapidMove(i, Player1), p2a.ExpectedRapidMove(i, Player2));

                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        yield break;
                    }

                    //  先制攻撃処理
                    yield return ResolveRapidAttack(p1a, p2a, i);

                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        yield break;
                    }

                    yield return ResolveCounter(p1a, p2a, i);
                    yield return ResolveActionAttack(p1a, p2a, i);

                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        yield break;
                    }

                    //  後手移動処理
                    yield return ResolveMove(
                        Player1.Gradiator.RelativePosToAbsolute(p1a.ExpectedActionMove(i, Player1).ToRelativePos()),
                        Player2.Gradiator.RelativePosToAbsolute(p2a.ExpectedActionMove(i, Player2).ToRelativePos()));

                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        yield break;
                    }

                    //  相手移動処理
                    yield return ResolveMove(p1a.ExpectedEnemyMove(i, Player1), p2a.ExpectedEnemyMove(i, Player2));

                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        yield break;
                    }

                }

                TurnEnd();
                DealCards();
                yield return null;
            }
        }

        public void TurnEnd() {
            //  プロット状態を更新
            Player1.Plots.OnTurnEnd(trash);
            Player2.Plots.OnTurnEnd(trash);

            //  切り札を除く手札を捨てる
            Player1.Hand.TrashExcludeTrump(trash);
            Player2.Hand.TrashExcludeTrump(trash);
        }



        //  移動プロット
        //  プロットしない箇所は-1
        public void MovePlotDebug(int[] plots) {
            MovePlot(MyPlayer, plots);

            int[] enemyPlots = new int[rule.CountOfMoment.Value];
            for (int i = 0; i < enemyPlots.Length; ++i) {
                if (EnemyPlayer.Plots.IsMovePenarized(i)) {
                    enemyPlots[i] = -1;
                }
                else {
                    enemyPlots[i] = EnemyPlayer.Hand.Cards[i].Id;
                }
            }
            MovePlot(EnemyPlayer, enemyPlots);

        }

        public void ActionPlotDebug(int[] plots) {
            ActionPlot(MyPlayer, plots);

            int[] enemyPlots = new int[rule.CountOfMoment.Value];
            for (int i = 0; i < enemyPlots.Length; ++i) {
                if (EnemyPlayer.Plots.IsActionPenarized(i)) {
                    enemyPlots[i] = -1;
                }
                else {
                    enemyPlots[i] = EnemyPlayer.Hand.Cards[i].Id;
                }
            }
            ActionPlot(EnemyPlayer, enemyPlots);

        }
    }
}
