using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    //  墓地
    public interface ITrash {

        //  カードリスト
        IList<ICard> Cards { get; }

        //  1枚捨てる
        void TrashCard(ICard card);
    }
}
