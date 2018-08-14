using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    public interface ICardSet {

        //  カードID → カード参照の取得
        ICard GetCard(int cardId);

    }

    public class CardSet : ICardSet{

        //  ランダムアクセス可能なデータ構造を使う
        private List<ICard> cards = new List<ICard>(); 

        public void CreateCardSet() {

        }

        public ICard GetCard(int cardId) {
            return cards[cardId];
        }
    }
}
