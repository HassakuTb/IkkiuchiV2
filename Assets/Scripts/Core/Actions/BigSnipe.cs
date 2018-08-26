using System.Linq;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  精密射撃
    [CreateAssetMenu(menuName = "Actions/BigSnipe", fileName = "BigSnipe")]
    public class BigSnipe : Action{

        public override int ResolveDamage(int momentIndex, IPlayer player) {

            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (player == Controller.Player1) {
                if (!Controller.Player2.Plots.IsMovePloted(momentIndex)) {
                    if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                        return 3;
                    }
                    return 0;
                }
                else {
                    return 0;
                }
            }
            else {
                if (!Controller.Player1.Plots.IsMovePloted(momentIndex)) {
                    if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                        return 3;
                    }
                    return 0;
                }
                else {
                    return 0;
                }
            }
        }

        public override string GetDetailText() {
            return "相手が移動プロットを上書きしているか、移動ペナルティのとき、3のダメージ";
        }
    }
}
