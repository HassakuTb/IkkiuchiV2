using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    //  共通デッキ
    public interface IDeck {

        //  カードを一枚ドロー
        ICard Draw();

        //  カードを後ろに追加
        void AppendCards(IEnumerable<ICard> src);

        //  デッキが空か？
        bool IsEmpty();
    }
}
