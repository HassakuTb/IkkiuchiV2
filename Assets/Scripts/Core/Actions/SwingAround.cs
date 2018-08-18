using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  振り回し
    [CreateAssetMenu(menuName = "Actions/SwingAround", fileName = "SwingAround")]
    public class SwingAround : Damage {

        //  全周囲
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-1, -1);
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(1, -1);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(1, 1);
        }
    }
}
