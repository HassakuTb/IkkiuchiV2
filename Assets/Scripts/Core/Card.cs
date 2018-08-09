namespace IkkiuchiV2.Core {
    public interface ICard {

        //  移動方向
        Direction MoveDirection { get; }

        //  上書きアクション
        IAction Action { get; }
    }
}
