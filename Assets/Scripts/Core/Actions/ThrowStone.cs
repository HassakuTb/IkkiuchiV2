using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  投石
    [CreateAssetMenu(menuName = "Actions/ThrowStone", fileName = "ThrowStone")]
    public class ThrowStone : Damage {

        //  1マスおき8方向
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-2, -2);
            yield return new RelativePos(-2, 0);
            yield return new RelativePos(-2, 2);
            yield return new RelativePos(0, -2);
            yield return new RelativePos(0, 2);
            yield return new RelativePos(2, -2);
            yield return new RelativePos(2, 0);
            yield return new RelativePos(2, 2);
        }
    }
}
