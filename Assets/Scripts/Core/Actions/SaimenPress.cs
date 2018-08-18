using System.Collections.Generic;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  西面プレス
    [CreateAssetMenu(menuName = "Actions/SaimenPress", fileName = "SaimenPress")]
    public class SaimenPress : Damage{

        //  真後ろのみ
        protected override IEnumerable<RelativePos> EnumerateRange() {
            yield return new RelativePos(0, -1);
        }
    }
}
