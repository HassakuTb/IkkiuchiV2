using System.Collections.Generic;
using System.Linq;

namespace Ikkiuchi.Core {
    //  共通デッキ
    public interface IDeck {

        //  カードを一枚ドロー
        ICard Deal();

        //  切り札を一枚ドロー
        ICard DealTrump();

        //  カードを後ろに追加
        void AppendCards(IEnumerable<ICard> src);
        void AppendTrumps(IEnumerable<ICard> src);

        //  デッキが空か？
        bool IsEmpty();
    }

    public class Deck : IDeck {

        private Queue<ICard> Cards { get; set; } = new Queue<ICard>();
        private Queue<ICard> Trumps { get; set; } = new Queue<ICard>(); 

        public void AppendCards(IEnumerable<ICard> src) {
            src.ForEach(c => {
                Cards.Enqueue(c);
            });
        }

        public void AppendTrumps(IEnumerable<ICard> src) {
            src.ForEach(c => {
                Trumps.Enqueue(c);
            });
        }

        public ICard Deal() {
            return Cards.Dequeue();
        }

        public ICard DealTrump() {
            return Trumps.Dequeue();
        }

        public bool IsEmpty() {
            return !Cards.Any();
        }
    }
}
