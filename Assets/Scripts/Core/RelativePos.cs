namespace IkkiuchiV2.Core {
    //  相対位置
    public struct RelativePos{

        public RelativePos(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
