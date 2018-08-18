using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  突
    [CreateAssetMenu(menuName = "Actions/Pierce", fileName = "Pierce")]
    public class Pierce : Damage {

        //  斜め1マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, -1);
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(1, -1);
            yield return new RelativePos(1, 1);
        }
    }
}
