using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  ダッシメン
    [CreateAssetMenu(menuName = "Actions/Cotton", fileName = "Cotton")]
    public class Cotton : Action{

        public override void ResolveCounter(int momentIndex, IPlayer player) {
            player.Cotton = true;
        }

        public override string GetDetailText() {
            return "この瞬間にダメージを受けたとき、1回復";
        }
    }
}
