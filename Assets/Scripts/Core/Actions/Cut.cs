using System.Collections.Generic;
using UnityEngine;

namespace IkkiuchiV2.Core.Actions {
    //  斬
    [CreateAssetMenu(menuName = "Actions/Cut", fileName = "Cut")]
    public class Cut : Damage {

        //  十字方向1マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(0, 1);
        }
    }
}
