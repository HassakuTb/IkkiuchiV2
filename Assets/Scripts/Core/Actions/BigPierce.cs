using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  大突
    [CreateAssetMenu(menuName = "Actions/BigPierce", fileName = "BigPierce")]
    public class BigPierce : Damage {

        //  斜め1マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, -1);
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(1, -1);
            yield return new RelativePos(1, 1);
        }
    }
}
