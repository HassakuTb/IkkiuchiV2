using System.Collections.Generic;

namespace IkkiuchiV2.Core.Actions {
    //  単純ダメージ
    public abstract class Damage : Action{

        public int damageValue;

        protected abstract IEnumerable<RelativePos> EnumerateRange();
    }
}
