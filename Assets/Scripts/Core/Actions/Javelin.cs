using System.Collections.Generic;
using UnityEngine;

namespace IkkiuchiV2.Core.Actions {
    //  投槍
    [CreateAssetMenu(menuName = "Actions/Javelin", fileName = "Javelin")]
    public class Javelin : Damage {

        //  前方斜め2マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(-2, 2);
            yield return new RelativePos(1, 1);
            yield return new RelativePos(2, 2);
        }
    }
}
