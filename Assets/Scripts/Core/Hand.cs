using System.Collections.Generic;
using System.Linq;

namespace Ikkiuchi.Core {
    public interface IHand {

        //  所持カードリスト
        IList<ICard> Cards { get; }

        //  切り札を除く手札をゴミ箱へ
        void TrashExcludeTrump(ITrash trash);
    }

    public class Hand : IHand {

        private List<ICard> cards { get; set; } = new List<ICard>();

        public IList<ICard> Cards {
            get {
                return cards;
            }
        }

        public void TrashExcludeTrump(ITrash trash) {
            cards.Where(c => !c.Action.IsTrump)
                .ForEach(c => {
                    trash.TrashCard(c);
                });

            cards.RemoveAll(c => !c.Action.IsTrump);
        }
    }
}
