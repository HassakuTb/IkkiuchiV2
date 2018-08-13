using System.Collections.Generic;
using UnityEngine;

namespace IkkiuchiV2.Core.Actions {
    //  ガルブラスター
    [CreateAssetMenu(menuName = "Actions/GalBlaster", fileName = "GalBlaster")]
    public class GalBlaster : Damage {

        //  横2マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-2, 0);
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(2, 0);
        }
    }
}
