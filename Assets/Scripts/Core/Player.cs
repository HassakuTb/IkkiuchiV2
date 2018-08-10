namespace IkkiuchiV2.Core {
    //  ゲームルールとしてのプレイヤー
    public interface IPlayer {

        //  駒への参照
        IGradiator Gradiator { get; }
    }

    public class Player : IPlayer {




        public IGradiator Gradiator {
            get {
                throw new System.NotImplementedException();
            }
        }
    }

}
