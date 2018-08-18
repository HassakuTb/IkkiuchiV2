using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  小銃
    [CreateAssetMenu(menuName = "Actions/Rifle", fileName = "Rifle")]
    public class Rifle : Damage {
        
        //  十字方向2マスぶん
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-2, 0);
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(2, 0);
            yield return new RelativePos(0, -2);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(0, 2);
        }
    }
}
