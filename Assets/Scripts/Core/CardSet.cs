using System.Collections.Generic;
using System.Linq;

namespace Ikkiuchi.Core {
    public interface ICardSet {

        //  カードID → カード参照の取得
        ICard GetCard(int cardId);

        //  切り札以外のカードの取得
        IList<ICard> EnumerateNormalCards();

        //  全切り札の取得
        IList<ICard> EnumerateTrumps();
    }

    public class CardSet : ICardSet{

        public class Factory {

            public ICardSet CreateClassic() {
                CardSet set = new CardSet();

                ClassicSetDefinition().ForEach(c => {
                    set.cards.Add(c);
                });

                return set;
            }

            private IEnumerable<ICard> ClassicSetDefinition() {
                int i = 0;
                yield return new Card(i++, null, Direction.None); //  TODO;
            }
        }

        private CardSet() { }

        //  ランダムアクセス可能なデータ構造を使う
        private List<ICard> cards = new List<ICard>();

        public ICard GetCard(int cardId) {
            return cards[cardId];
        }

        public IList<ICard> EnumerateNormalCards() {
            return cards.Where(c => !c.Action.IsTrump).ToList();
        }

        public IList<ICard> EnumerateTrumps() {
            return cards.Where(c => c.Action.IsTrump).ToList();
        }
    }
}
