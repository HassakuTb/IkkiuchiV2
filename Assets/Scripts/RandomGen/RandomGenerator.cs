
using System;

namespace RandomGen {
    [Serializable]
    public abstract class RandomGenerator {

        private static RandomGenerator instance;

        public static RandomGenerator Instance {
            get {
                if (instance == null) instance = new XorShift128();
                return instance;
            }
        }

        /// <summary>
        /// シード値
        /// </summary>
        public abstract uint Seed {
            get;
            set;
        }

        /// <summary>
        /// 0 ～ uint.MaxValueの乱数を取得
        /// </summary>
        /// <returns>[0U, uint.MaxValue]</returns>
        public abstract uint GetUint();

        ///    Get random float value
        ///    returns float [0.0, 1.0]
        ///    needs GetUInt() in self
        ///    refs http://marupeke296.com/TIPS_No16_flaotrandom.html
        ///    ランダムなfloat型の値を取得する[0.0f, 1.0f]
        public float GetFloat() {
            uint floatBits = (this.GetUint() >> 9) | 0x3f800000;
            return BitConverter.ToSingle(BitConverter.GetBytes(floatBits), 0) - 1.0f;
        }


    }

    ///    Random value generator uisng XorShift algorithm
    ///    周期2^128版
    ///
    ///    paper
    ///    http://www.jstatsoft.org/v08/i14/paper
    ///
    [Serializable]
    public class XorShift128 : RandomGenerator {

        private uint grandSeed;
        private uint[] seed = new uint[4];

        public XorShift128() : this(0U) {
        }

        public XorShift128(uint seed) {
            Seed = seed;
        }

        public override uint Seed {
            get { return this.grandSeed; }
            set {
                this.grandSeed = value;
                for (uint i = 0; i < 4; ++i) {
                    seed[i] = value = 1812433253U * (value ^ (value >> 30)) + i + 1;
                }
            }
        }

        public override uint GetUint() {
            uint t, w;
            t = seed[0];
            seed[0] = seed[1];
            seed[1] = seed[2];
            seed[2] = w = seed[3];
            t = t ^ (t << 11);
            w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
            seed[3] = w;
            return w;
        }

    }
}
