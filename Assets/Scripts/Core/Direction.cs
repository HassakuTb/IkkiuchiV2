namespace IkkiuchiV2.Core {

    //  相対方向
    public enum Direction {
        None,
        Front,
        Back,
        Left,
        Right,
    }

    public static class DirectionExtensions {
        public static RelativePos ToRelativePos(this Direction dir) {
            switch (dir) {
                case Direction.None:
                    return new RelativePos(0, 0);
                case Direction.Front:
                    return new RelativePos(0, 1);
                case Direction.Back:
                    return new RelativePos(0, -1);
                case Direction.Left:
                    return new RelativePos(-1, 0);
                case Direction.Right:
                    return new RelativePos(1, 0);
                default:
                    return new RelativePos(0, 0);
            }
        }
    }

    public enum GradiatorDirection {
        North,  //  北向き(Player1)
        South,  //  南向き(Player2)
    }
}
