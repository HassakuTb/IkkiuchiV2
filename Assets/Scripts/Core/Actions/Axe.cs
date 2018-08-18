using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  斧
    [CreateAssetMenu(menuName = "Actions/Axe", fileName = "Axe")]
    public class Axe : Damage {

        //  左斜め前とその逆
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(1, -1);
        }
    }
}
