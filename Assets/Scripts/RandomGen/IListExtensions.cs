using System.Collections.Generic;
using System.Linq;

namespace RandomGen {
    public static class IListExtensions {

        public static IList<T> Shuffle<T>(this IList<T> list, RandomGenerator rgen) {
            IList<T> target = list.ToList();
            for (int i = target.Count; i > 1; --i) {
                int a = i - 1;
                int b = (int)(rgen.GetUint() % i);
                
                T tmp = target[a];
                target[a] = target[b];
                target[b] = tmp;
            }

            return target;
        }
    }
}
