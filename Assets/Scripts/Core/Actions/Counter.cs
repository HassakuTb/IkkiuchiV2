using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  カウンター
    [CreateAssetMenu(menuName = "Actions/Counter", fileName = "Counter")]
    public class Counter : Action{

        public override void ResolveCounter(int momentIndex, IPlayer player) {
            player.Counter = true;
        }

        public override string GetDetailText() {
            return "この瞬間にダメージを受けたとき、相手にも受けたダメージを与える";
        }
    }
}
