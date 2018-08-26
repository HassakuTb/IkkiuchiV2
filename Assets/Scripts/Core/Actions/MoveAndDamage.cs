using System.Linq;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  移動攻撃(ムーンサルト)
    public abstract class MoveAndDamage : Action{

        public int damageValue;
        public Direction actionMove;

        public override Direction ActionMoveDirection {
            get {
                return actionMove;
            }
        }

        public override Direction ExpectedActionMove(int momentIndex, IPlayer player) {
            return actionMove;
        }

        public override int ResolveDamage(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                return damageValue;
            }
            else {
                return 0;
            }
        }

        public override string GetDetailText() {
            return string.Format("{0}のダメージの後、{1}方向に移動", damageValue, actionMove.ToJpString());
        }
    }
}
