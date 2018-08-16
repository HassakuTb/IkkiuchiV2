namespace IkkiuchiV2.Core {
    //  絶対位置
    public struct Pos {

        public Pos(int x, int y) {
            X = x; Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        //  ボード内の座標かどうか
        public bool IsInboundBoard() {
            if (X < 0) return false;
            if (X >= Board.SizeX) return false;
            if (Y < 0) return false;
            if (Y >= Board.SizeY) return false;
            return true;
        }

        public static bool operator ==(Pos left, Pos right) {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Pos left, Pos right) {
            return !(left == right);
        }
    }
}
