using RandomGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Ikkiuchi.Core {

    public struct DamageEffectParam {
        public Pos Pos { get; set; }
        public int Value { get; set; }
    }

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

        public bool P1MoveAnimation { get; set; }
        public bool P2MoveAnimation { get; set; }
        public bool P1MoveFailAnimation { get; set; }
        public bool P2MoveFailAnimation { get; set; }
        public Pos P1MoveTo { get; set; }
        public Pos P2MoveTo { get; set; }
        public List<DamageEffectParam> DamageParams { get; set; } = new List<DamageEffectParam>();
        public List<Pos> Slashes { get; set; } = new List<Pos>();
        public List<Pos> Heal { get; set; } = new List<Pos>();
        public List<Pos> PowUp { get; set; } = new List<Pos>();

        public bool IsAnimationOver {
            get {
                if (P1MoveAnimation) return false;
                if (P2MoveAnimation) return false;
                if (P1MoveFailAnimation) return false;
                if (P2MoveFailAnimation) return false;
                if (DamageParams.Count > 0) return false;
                if (Slashes.Count > 0) return false;
                if (Heal.Count > 0) return false;
                if (PowUp.Count > 0) return false;
                return true;
            }
        }

        public void ResetAnimation() {
            P1MoveAnimation = false;
            P2MoveAnimation = false;
            P1MoveFailAnimation = false;
            P2MoveFailAnimation = false;
            DamageParams.Clear();
            Slashes.Clear();
            Heal.Clear();
            PowUp.Clear();
        }

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

            currentIndex = -1;
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

            if (player == Player1) {
                isP1MovePlotted = true;
            }
            else {
                isP2MovePlotted = true;
            }

            if (isP1MovePlotted && isP2MovePlotted) {
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

        private void ResolveRapidAttackAnim(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasRapidAttack()) {
                P1MoveFailAnimation = true;
                P1MoveTo = Player1.Gradiator.RelativePosToAbsolute(Direction.Front.ToRelativePos());
                DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = p1a.ResolveRapidAttack(momentIndex, Player1) });
                if (p1a.ResolveRapidAttack(momentIndex, Player1) > 0) Slashes.Add(Player2.Gradiator.Position);
            }

            if (p2a.HasRapidAttack()) {
                P2MoveFailAnimation = true;
                P2MoveTo = Player2.Gradiator.RelativePosToAbsolute(Direction.Front.ToRelativePos());
                DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = p2a.ResolveRapidAttack(momentIndex, Player2) });
                if (p2a.ResolveRapidAttack(momentIndex, Player2) > 0) Slashes.Add(Player1.Gradiator.Position);
            }
        }

        private void ResolveRapidAttack(IAction p1a, IAction p2a, int momentIndex) {
            //  モデル更新
            Player1.Life.DealDamage(p2a.ResolveRapidAttack(momentIndex, Player2));
            Player2.Life.DealDamage(p1a.ResolveRapidAttack(momentIndex, Player1));
        }

        private void ResolveCounterAnim(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasCounter()) {
                PowUp.Add(Player1.Gradiator.Position);
            }
            if (p2a.HasCounter()) {
                PowUp.Add(Player2.Gradiator.Position);
            }
        }

        private void ResolveCounter(IAction p1a, IAction p2a, int momentIndex) {
            p1a.ResolveCounter(momentIndex, Player1);
            p2a.ResolveCounter(momentIndex, Player2);
        }




        private void ResolveMoveAnim(Pos p1to, Pos p2to) {

            bool p1Move = p1to != Player1.Gradiator.Position;
            bool p2Move = p2to != Player2.Gradiator.Position;
            if (p1Move && p2Move) {
                //  両方移動するとき
                //  移動先が同じまたはp1の移動先がp2の位置かつp2の移動先がp1の位置(交差)ならば衝突としてマーク
                bool isCollide = p1to == p2to ||
                    (p1to == Player2.Gradiator.Position && p2to == Player1.Gradiator.Position);

                if (!isCollide) {

                    //  p1が外に移動しようとするかつp2がp1の位置に移動しようとする→押し出し
                    if (!p1to.IsInboundBoard() && p2to == Player1.Gradiator.Position) {
                        //  衝突アニメーション
                        P1MoveFailAnimation = true;
                        P2MoveFailAnimation = true;
                        P1MoveTo = p1to;
                        P2MoveTo = p2to;
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position , Value = 1});
                    }
                    else if (!p2to.IsInboundBoard() && p1to == Player2.Gradiator.Position) {
                        //  衝突アニメーション
                        P1MoveFailAnimation = true;
                        P2MoveFailAnimation = true;
                        P1MoveTo = p1to;
                        P2MoveTo = p2to;
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = 1 });
                    }
                    else {
                        //  衝突しないとき
                        if (!p1to.IsInboundBoard()) {
                            P1MoveFailAnimation = true;
                            P1MoveTo = p1to;
                            DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = 1 });
                        }
                        else {
                            P1MoveAnimation = true;
                            P1MoveTo = p1to;
                        }
                        //  衝突しないとき
                        if (!p2to.IsInboundBoard()) {
                            P2MoveFailAnimation = true;
                            P2MoveTo = p2to;
                            DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = 1 });
                        }
                        else {
                            P2MoveAnimation = true;
                            P2MoveTo = p2to;
                        }
                    }
                }
                else {

                    //  衝突アニメーション
                    P1MoveFailAnimation = true;
                    P2MoveFailAnimation = true;
                    P1MoveTo = p1to;
                    P2MoveTo = p2to;
                }
            }
            else if (p1Move) {
                //  p1のみ移動するとき
                if (p1to == p2to) {  //  P2を押し込む場合
                    RelativePos relative = new RelativePos(p1to.X - Player1.Gradiator.Position.X, p1to.Y - Player1.Gradiator.Position.Y);
                    if (new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  アニメーション
                        P1MoveAnimation = true;
                        P2MoveAnimation = true;
                        P1MoveTo = p1to;
                        P2MoveTo = new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  アニメーション
                        P1MoveFailAnimation = true;
                        P2MoveFailAnimation = true;
                        P1MoveTo = p1to;
                        P2MoveTo = new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y);
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = 1 });
                    }

                }
                else {  //  通常移動
                        //  アニメーション
                    if (p1to.IsInboundBoard()) {
                        P1MoveAnimation = true;
                        P1MoveTo = p1to;
                    }
                    else {
                        P1MoveFailAnimation = true;
                        P1MoveTo = p1to;
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = 1 });
                    }
                }
            }
            else if (p2Move) {
                //  p2のみ移動するとき
                if (p1to == p2to) {  //  P1を押し込む場合
                    RelativePos relative = new RelativePos(p2to.X - Player2.Gradiator.Position.X, p2to.Y - Player2.Gradiator.Position.Y);
                    if (new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  アニメーション
                        P1MoveAnimation = true;
                        P2MoveAnimation = true;
                        P2MoveTo = p2to;
                        P1MoveTo = new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  アニメーション
                        P1MoveFailAnimation = true;
                        P2MoveFailAnimation = true;
                        P2MoveTo = p2to;
                        P1MoveTo = new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y);
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = 1 });
                    }
                }
                else {  //  通常移動
                    //  アニメーション
                    if (p2to.IsInboundBoard()) {
                        P2MoveAnimation = true;
                        P2MoveTo = p2to;
                    }
                    else {
                        P2MoveFailAnimation = true;
                        P2MoveTo = p2to;
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = 1 });
                    }
                }
            }
        }

        private void ResolveMove(Pos p1to, Pos p2to) {

            bool p1Move = p1to != Player1.Gradiator.Position;
            bool p2Move = p2to != Player2.Gradiator.Position;
            if (p1Move && p2Move) {
                //  両方移動するとき
                //  移動先が同じまたはp1の移動先がp2の位置かつp2の移動先がp1の位置(交差)ならば衝突としてマーク
                bool isCollide = p1to == p2to ||
                    (p1to == Player2.Gradiator.Position && p2to == Player1.Gradiator.Position);
                //  モデル更新
                if (!isCollide) {
                    //  p1が外に移動しようとするかつp2がp1の位置に移動しようとする→押し出し
                    if(!p1to.IsInboundBoard() && p2to == Player1.Gradiator.Position) {
                        Player1.Life.DealDamage(1);
                    }
                    else if(!p2to.IsInboundBoard() && p1to == Player2.Gradiator.Position) {
                        Player2.Life.DealDamage(1);
                    }
                    else {
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
            }
            else if (p1Move) {
                //  p1のみ移動するとき
                if (p1to == p2to) {  //  P2を押し込む場合
                    RelativePos relative = new RelativePos(p1to.X - Player1.Gradiator.Position.X, p1to.Y - Player1.Gradiator.Position.Y);
                    if (new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  モデル更新
                        Player1.Gradiator.Position = p1to;
                        Player2.Gradiator.Position = new Pos(Player2.Gradiator.Position.X + relative.X, Player2.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  モデル更新
                        Player2.Life.DealDamage(1);
                    }

                }
                else {  //  通常移動
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
                    RelativePos relative = new RelativePos(p2to.X - Player2.Gradiator.Position.X, p2to.Y - Player2.Gradiator.Position.Y);
                    if (new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y).IsInboundBoard()) {   //  押し込み可能
                        //  モデル更新
                        Player2.Gradiator.Position = p2to;
                        Player1.Gradiator.Position = new Pos(Player1.Gradiator.Position.X + relative.X, Player1.Gradiator.Position.Y + relative.Y);
                    }
                    else {  //  後が無い
                        //  モデル更新
                        Player1.Life.DealDamage(1);
                    }

                }
                else {  //  通常移動
                    //  モデル更新
                    if (p2to.IsInboundBoard()) {
                        Player2.Gradiator.Position = p2to;
                    }
                    else {
                        Player2.Life.DealDamage(1);
                    }
                }
            }
        }


        private void ResolveActionAttackAnim(IAction p1a, IAction p2a, int momentIndex) {
            if (p1a.HasActionAttack()) {
                P1MoveFailAnimation = true;
                P1MoveTo = Player1.Gradiator.RelativePosToAbsolute(Direction.Front.ToRelativePos());
                if (p1a.ResolveDamage(momentIndex, Player1) == 0) {
                    DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = 0 });
                    //  miss
                }
                else {
                    if (Player2.BigCounter) {
                        Slashes.Add(Player1.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = p1a.ResolveDamage(momentIndex, Player1) * 2 });
                    }
                    else {
                        // damage
                        Slashes.Add(Player2.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = p1a.ResolveDamage(momentIndex, Player1) });

                    }
                    if (Player2.Cotton) {
                        Heal.Add(Player2.Gradiator.Position);
                    }
                    if (Player2.Counter) {
                        Slashes.Add(Player1.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = p1a.ResolveDamage(momentIndex, Player1) });
                    }
                }
            }

            if (p2a.HasActionAttack()) {
                P2MoveFailAnimation = true;
                P2MoveTo = Player2.Gradiator.RelativePosToAbsolute(Direction.Front.ToRelativePos());
                if (p2a.ResolveDamage(momentIndex, Player2) == 0) {
                    DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = 0 });
                    //  miss
                }
                else {
                    if (Player1.BigCounter) {
                        Slashes.Add(Player2.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = p2a.ResolveDamage(momentIndex, Player2) * 2 });
                    }
                    else {
                        // damage
                        Slashes.Add(Player1.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player1.Gradiator.Position, Value = p2a.ResolveDamage(momentIndex, Player2) });

                    }
                    if (Player1.Cotton) {
                        Heal.Add(Player1.Gradiator.Position);
                    }
                    if (Player1.Counter) {
                        Slashes.Add(Player2.Gradiator.Position);
                        DamageParams.Add(new DamageEffectParam { Pos = Player2.Gradiator.Position, Value = p2a.ResolveDamage(momentIndex, Player2) });
                    }
                }
            }
        }


        private void ResolveActionAttack(IAction p1a, IAction p2a, int momentIndex) {
            //  モデル更新
            if (p1a.HasActionAttack()) {

                if (Player2.BigCounter) {
                    Player1.Life.DealDamage(p1a.ResolveDamage(momentIndex, Player1) * 2);
                }
                else {
                    // damage
                    Player2.Life.DealDamage(p1a.ResolveDamage(momentIndex, Player1));
                }
                if (Player2.Cotton) {
                    Player2.Life.DealDamage(-1);
                }
                if (Player2.Counter) {
                    Player1.Life.DealDamage(p1a.ResolveDamage(momentIndex, Player1));
                }
            }

            if (p2a.HasActionAttack()) {
                if (Player1.BigCounter) {
                    Player2.Life.DealDamage(p2a.ResolveDamage(momentIndex, Player2) * 2);
                }
                else {
                    // damage
                    Player1.Life.DealDamage(p2a.ResolveDamage(momentIndex, Player2));
                }
                if (Player1.Cotton) {
                    Player1.Life.DealDamage(-1);
                }
                if (Player1.Counter) {
                    Player2.Life.DealDamage(p2a.ResolveDamage(momentIndex, Player2));
                }
            }
        }

        private bool IsSettled() {
            return Player1.Life.IsDead || Player2.Life.IsDead;
        }

        private int currentIndex;
        private int resolveState;

        public int CurrentIndex {
            get {
                return currentIndex;
            }
            set {
                currentIndex = value;
            }
        }

        //  解決
        public void Resolve() {
            if (!IsAnimationOver) return;

            if(currentIndex >= rule.CountOfMoment.Value) {
                TurnEnd();
                DealCards();
                CurrentPhase = Phase.MovePlot;
                resolveState = 0;
                currentIndex = -1;
                return;
            }

            if (currentIndex < 0) return;

            IList<IAction> p1Actions = Player1.GetActualActions(rule.CountOfMoment.Value, this);
            IList<IAction> p2Actions = Player2.GetActualActions(rule.CountOfMoment.Value, this);

            int cindex = currentIndex < 0 ? 0 : currentIndex;
            IAction p1a = p1Actions[cindex];
            IAction p2a = p2Actions[cindex];

            switch (resolveState) {
                case 0:

                    //  カウンター状態のクリア
                    Player1.Cotton = false;
                    Player1.BigCounter = false;
                    Player1.Counter = false;
                    Player2.Cotton = false;
                    Player2.BigCounter = false;
                    Player2.Counter = false;

                    //  先制移動処理
                    ResolveMoveAnim(p1a.ExpectedRapidMove(cindex, Player1), p2a.ExpectedRapidMove(cindex, Player2));

                    resolveState = 1;
                    break;
                case 1:

                    ResolveMove(p1a.ExpectedRapidMove(cindex, Player1), p2a.ExpectedRapidMove(cindex, Player2));
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 2;
                    break;

                case 2:

                    //  先制攻撃処理
                    ResolveRapidAttackAnim(p1a, p2a, cindex);

                    resolveState = 3;
                    break;

                case 3:

                    //  先制攻撃処理
                    ResolveRapidAttack(p1a, p2a, cindex);
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 4;
                    break;

                case 4:
                    ResolveCounterAnim(p1a, p2a, cindex);

                    resolveState = 5;
                    break;

                case 5:
                    ResolveCounter(p1a, p2a, cindex);
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 6;
                    break;

                case 6:
                    ResolveActionAttackAnim(p1a, p2a, cindex);

                    resolveState = 7;
                    break;

                case 7:
                    ResolveActionAttack(p1a, p2a, cindex);
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 8;
                    break;

                case 8:
                    ResolveMoveAnim(
                    Player1.Gradiator.RelativePosToAbsolute(p1a.ExpectedActionMove(cindex, Player1).ToRelativePos()),
                    Player2.Gradiator.RelativePosToAbsolute(p2a.ExpectedActionMove(cindex, Player2).ToRelativePos()));

                    resolveState = 9;
                    break;

                case 9:
                    ResolveMove(
                    Player1.Gradiator.RelativePosToAbsolute(p1a.ExpectedActionMove(cindex, Player1).ToRelativePos()),
                    Player2.Gradiator.RelativePosToAbsolute(p2a.ExpectedActionMove(cindex, Player2).ToRelativePos()));
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 10;
                    break;

                case 10:
                    ResolveMoveAnim(p2a.ExpectedEnemyMove(cindex, Player2), p1a.ExpectedEnemyMove(cindex, Player1));

                    resolveState = 11;
                    break;

                case 11:
                    ResolveMove(p2a.ExpectedEnemyMove(cindex, Player2), p1a.ExpectedEnemyMove(cindex, Player1));
                    //  決着判定
                    if (IsSettled()) {
                        CurrentPhase = Phase.Settle;
                        resolveState = 0;
                        return;
                    }

                    resolveState = 12;
                    break;

                case 12:
                    resolveState = 0;
                    currentIndex++;
                    break;

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
                enemyPlots[i] = -1;
            }
            ActionPlot(EnemyPlayer, enemyPlots);

        }
    }
}
