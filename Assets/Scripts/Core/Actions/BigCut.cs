using System.Collections.Generic;
using UnityEngine;

namespace IkkiuchiV2.Core.Actions {
    //  大斬
    [CreateAssetMenu(menuName = "Actions/BigCut", fileName = "BigCut")]
    public class BigCut : Damage {

        //  十字方向
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(0, 1);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(-1, 0);
        }
    }
}
