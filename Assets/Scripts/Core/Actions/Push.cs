using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  押
    [CreateAssetMenu(menuName = "Actions/Push", fileName = "Push")]
    public class Push : Action {

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

        public override Pos ExpectedEnemyMove(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateDamageRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                RelativePos relative = new RelativePos(
                    enemy.Gradiator.Position.X - player.Gradiator.Position.X,
                    enemy.Gradiator.Position.Y - player.Gradiator.Position.Y
                    );
                return new Pos(enemy.Gradiator.Position.X + relative.X, enemy.Gradiator.Position.Y + relative.Y);
            }
            else {
                return enemy.Gradiator.Position;
            }
        }

        public override string GetDetailText() {
            return "1のダメージの後、相手を押し込む";
        }
    }
}
