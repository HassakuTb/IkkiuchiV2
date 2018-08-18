
namespace Ikkiuchi.Core {
    //  ライフ
    public interface ILife {

        int Max { get; }

        int Value { get; }

        bool IsDead { get; }

        void DealDamage(int damageValue);
    }

    public class Life : ILife {

        public Life(int maxValue) {
            Max = maxValue;
            Value = maxValue;
        }

        public int Max { get; private set; }
        public int Value { get; private set; }

        public bool IsDead {
            get {
                return Value <= 0;
            }
        }

        public void DealDamage(int damageValue) {
            Value -= damageValue;
            if (Value < 0) Value = 0;
        }
    }
}
