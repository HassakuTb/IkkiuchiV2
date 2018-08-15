
namespace RandomGen {
    public interface IRandomGenerator {

        void SetSeed(int seed);

        int Next();
    }
}
