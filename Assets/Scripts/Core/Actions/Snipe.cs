using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  狙撃
    [CreateAssetMenu(menuName = "Actions/Snipe", fileName = "Snipe")]
    public class Snipe : Damage {

        //  8方向2マス分 + 前後3マス
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(-2, -2);
            yield return new RelativePos(-1, -1);
            yield return new RelativePos(0, -2);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(-2, 2);
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(0, 2);
            yield return new RelativePos(1, 1);
            yield return new RelativePos(2, 2);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(2, 0);
            yield return new RelativePos(1, -1);
            yield return new RelativePos(2, -2);
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(-2, 0);
            yield return new RelativePos(0, -3);
            yield return new RelativePos(0, 3);
        }
    }
}
