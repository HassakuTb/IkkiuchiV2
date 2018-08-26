using System.Collections.Generic;
using System.Linq;

namespace Ikkiuchi.Core.Actions {
    //  単純ダメージ
    public abstract class Damage : Action{

        public int damageValue;

        protected abstract IEnumerable<RelativePos> EnumerateRange();

        public override int ResolveDamage(int momentIndex, IPlayer player) {
            IPlayer enemy = player == Controller.Player1 ? Controller.Player2 : Controller.Player1;
            if (EnumerateRange().Select(r => player.Gradiator.RelativePosToAbsolute(r)).Contains(enemy.Gradiator.Position)) {
                return damageValue;
            }
            else {
                return 0;
            }
        }

        public override string GetDetailText() {
            return string.Format("{0}のダメージ", damageValue);
        }

        public override IEnumerable<RelativePos> EnumerateDamageRange() {
            return EnumerateRange();
        }
    }
}
