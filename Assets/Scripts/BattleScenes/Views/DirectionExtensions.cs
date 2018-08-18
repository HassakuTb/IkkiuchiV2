using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    public static class DirectionExtensions {

        public static float ToRotateZ(this Direction dir) {
            switch (dir) {
                case Direction.Front:
                    return 0f;
                case Direction.Left:
                    return 90f;
                case Direction.Back:
                    return 180f;
                case Direction.Right:
                    return 270f;
                default:
                    return 0f;
            }
        }
    }
}
