using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  方向転換
    [CreateAssetMenu(fileName ="ChangeDirection", menuName="Actions/ChangeDirection")]
    public class ChangeDirection : Action{

        public Direction direction;

        public override Direction ActionMoveDirection {
            get {
                return direction;
            }
        }

        public override Direction ExpectedActionMove(int momentIndex, IPlayer player) {
            return direction;
        }

        public override string GetDetailText() {
            return string.Format("{0}方向に移動する", direction.ToJpString());
        }
    }

    public static partial class DirectionExtensions {
        public static string ToJpString(this Direction dir) {
            switch (dir) {
                case Direction.Front:
                    return "前";
                case Direction.Left:
                    return "左";
                case Direction.Right:
                    return "右";
                case Direction.Back:
                    return "後";
                default:
                    return "";
            }
        }
    }
}
