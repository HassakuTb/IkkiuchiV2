using System.Collections.Generic;
using Ikkiuchi.Core.Actions;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.Core {
    //  ゲームルールとしてのプレイヤー
    public interface IPlayer {

        //  駒への参照
        IGradiator Gradiator { get; }

        //  ライフ
        ILife Life { get; }

        //  手札
        IHand Hand { get; }

        //  プロット
        IPlots Plots { get; }

        //  切り札を必要枚数ドロー
        void DrawTrump(IDeck deck);

        //  上書き後の実際の行動リストを取得
        IList<IAction> GetActualActions(int countOfMoment, Controller controller);

        //  ダッシメン状態
        bool Cotton { get; set; }

        //  カウンター状態
        bool Counter { get; set; }

        //  先の先状態
        bool BigCounter { get; set; }
    }

    public class Player : IPlayer {

        [Inject] private DiContainer container;

        //  引いた切り札の数
        private int drawedTramps = 0;

        //  切り札を必要枚数ドロー
        public void DrawTrump(IDeck deck) {
            if(Life.Value <= Life.Max / 2) {
                if(drawedTramps < 1) {
                    Hand.Cards.Add(deck.DealTrump());
                    drawedTramps++;
                }
            }
            if(Life.Value <= Life.Max / 4) {
                if(drawedTramps < 2) {
                    Hand.Cards.Add(deck.DealTrump());
                    drawedTramps++;
                }
            }
        }

        public Player(bool isPlayer1, IRule rule) {
            Life = new Life(rule.MaxLife);
            Pos initialPos = new Pos(Board.SizeX / 2, isPlayer1 ? 0 : Board.SizeY - 1);
            Gradiator = new Gradiator(initialPos, isPlayer1 ? GradiatorDirection.North : GradiatorDirection.South, this);
            Plots = new Plots(rule);
        }


        public IGradiator Gradiator { get; private set; }

        public ILife Life { get; private set; }

        public IHand Hand { get; private set; } = new Hand();

        public IPlots Plots { get; private set; }

        public bool Cotton { get; set; }
        public bool Counter { get; set; }
        public bool BigCounter { get; set; }

        public IList<IAction> GetActualActions(int countOfMoment, Controller controller) {
            IList<IAction> actual = new List<IAction>();
            for(int i = 0; i < countOfMoment; ++i) {
                //  攻撃がプロットされているとき
                if (Plots.IsActionPloted(i)) {
                    actual.Add(Plots.ActionPloted(i).Action);
                }
                //  移動がプロットされているとき
                else if (Plots.IsMovePloted(i)) {
                    Move act = ScriptableObject.Instantiate(Resources.Load<Move>("ScriptableObjecs/Actions/Move"));
                    act.Controller = controller;
                    act.Direction = Plots.MovePloted(i).MoveDirection;
                    actual.Add(act);
                }
                //  移動と攻撃の両方がプロットされていないとき
                else {
                    NoAction act = ScriptableObject.Instantiate(Resources.Load<NoAction>("ScriptableObjects/Actions/NoAction"));
                    act.Controller = controller;
                    actual.Add(act);
                }
            }

            return actual;
        }
    }

}
