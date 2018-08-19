using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  急襲
    [CreateAssetMenu(menuName = "Actions/Raid", fileName = "Raid")]
    public class Raid : Action{

        public override Pos ExpectedRapidMove(int momentIndex, IPlayer player) {
            return player.Gradiator.RelativePosToAbsolute(Direction.Front.ToRelativePos());
        }

        public override IEnumerable<RelativePos> EnumerateDamageRange() {
            yield return new RelativePos(-1, 1);
            yield return new RelativePos(0, 1);
            yield return new RelativePos(1, 1);
        }

        public override int ResolveDamage(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateDamageRange().Select(r => enemy.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                return 3;
            }
            else {
                return 0;
            }
        }

        public override string GetDetailText() {
            return "先に前方に移動してから、3のダメージ";
        }
    }
}
