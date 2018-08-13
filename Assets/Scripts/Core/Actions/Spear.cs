using System.Collections.Generic;
using UnityEngine;

namespace IkkiuchiV2.Core.Actions {
    //  槍
    [CreateAssetMenu(menuName = "Actions/Spear", fileName = "Spear")]
    public class Spear : Damage {

        //  前後2マス分
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(0, -2);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(0, 2);
        }
    }
}
