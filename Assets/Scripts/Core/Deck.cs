using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    //  共通デッキ
    public interface IDeck {

        //  カードリスト
        IList<ICard> Cards { get; }

        //  カードを一枚ドロー
        ICard Draw();
    }
}
