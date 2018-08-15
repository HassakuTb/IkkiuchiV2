using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    public interface IHand {

        //  所持カードリスト
        IList<ICard> Cards { get; }
    }

    public class Hand : IHand {

        public IList<ICard> Cards { get; private set; } = new List<ICard>();
    }
}
