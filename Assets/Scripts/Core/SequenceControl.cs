using Zenject;

namespace IkkiuchiV2.Core {
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

    //  DIインストーラ
    class SequenceControlInstaller : Installer<SequenceControlInstaller> {
        public override void InstallBindings() {
            Container.Bind<>
        }
    }

    //  シーケンスコントロール
    public class SequenceControl {

        //  プレイヤー1
        [Inject]
        private IPlayer player1;

        //  プレイヤー2
        [Inject]
        private IPlayer player2;


        

    }
}
