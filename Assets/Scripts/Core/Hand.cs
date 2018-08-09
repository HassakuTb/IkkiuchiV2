using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    public interface IHand {

        //  所持カードリスト
        IList<ICard> Cards { get; }
    }
}
