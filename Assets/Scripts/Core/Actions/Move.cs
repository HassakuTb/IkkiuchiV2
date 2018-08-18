using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  単純移動
    [CreateAssetMenu(menuName = "Actions/Move", fileName = "Move")]
    public class Move : Action{

        //  移動方向
        public Direction Direction { get; set; }

        public override RelativePos GetMoveTo() {
            return Direction.ToRelativePos();
        }
    }
}
