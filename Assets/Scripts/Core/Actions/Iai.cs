using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  一閃
    [CreateAssetMenu(menuName = "Actions/Iai", fileName = "Issen")]
    public class Iai : Action{

        public override IEnumerable<RelativePos> EnumerateDamageRange() {
            yield return new RelativePos(0, 3);
            yield return new RelativePos(0, 2);
            yield return new RelativePos(0, 1);
        }

        public override int ResolveRapidAttack(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                return 3;
            }
            else {
                return 0;
            }
        }

        public override string GetDetailText() {
            return "相手の行動よりも先に3のダメージ";
        }
    }
}
