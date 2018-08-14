using Zenject;

namespace IkkiuchiV2.Core {

    public interface IPhase {

    }

        //  フェーズ
    public enum Phase {
        Start,  //  開始フェーズ
        TurnStart,  //  ターン開始
        MovePlot,   //  移動プロット
        ActionPlot, //  行動プロット
        Resolve,    //  解決
        TurnEnd,    //  ターン終了
        Settle, //  決着
    }


    public interface ISequenceControl {

    }

    //  シーケンスコントロール
    public class SequenceControl {


        public void MakeBoard() {

        }



    }
}
