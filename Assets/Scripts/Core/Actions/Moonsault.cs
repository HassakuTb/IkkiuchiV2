using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  ムーンサルト
    [CreateAssetMenu(menuName = "Actions/Moonsault", fileName = "Moonsault")]
    public class Moonsault : MoveAndDamage{

        public override IEnumerable<RelativePos> EnumerateDamageRange() {
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(1, 1);
        }
    }
}
