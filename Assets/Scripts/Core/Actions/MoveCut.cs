using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  移斬
    [CreateAssetMenu(menuName = "Actions/MoveCut", fileName = "MoveCut")]
    public class MoveCut : Action{

        //  十字方向1マス
        public override IEnumerable<RelativePos> EnumerateDamageRange() {
            yield return new RelativePos(-1, 0);
            yield return new RelativePos(1, 0);
            yield return new RelativePos(0, -1);
            yield return new RelativePos(0, 1);
        }

        public override int ResolveDamage(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                return 1;
            }
            else {
                return 0;
            }
        }

        public override Direction ExpectedActionMove(int momentIndex, IPlayer player) {
            if (player == Controller.Player1) {
                if (Controller.Player1.Plots.IsMovePenarized(momentIndex)) {
                    return Direction.None;
                }
                return Controller.Player1.Plots.GetMovePlot(momentIndex).MoveDirection;
            }
            else {
                if (Controller.Player2.Plots.IsMovePenarized(momentIndex)) {
                    return Direction.None;
                }
                return Controller.Player2.Plots.GetMovePlot(momentIndex).MoveDirection;
            }
        }

        public override string GetDetailText() {
            return "1のダメージを与えた後、移動プロットの方向に移動する";
        }
    }
}
