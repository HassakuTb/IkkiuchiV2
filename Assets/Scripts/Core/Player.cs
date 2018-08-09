namespace IkkiuchiV2.Core {
    //  ゲームルールとしてのプレイヤー
    public interface IPlayer {

        //  駒への参照
        IGradiator Gradiator { get; }
    }
}
