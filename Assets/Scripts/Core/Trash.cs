using System.Collections.Generic;

namespace Ikkiuchi.Core {
    //  墓地
    public interface ITrash {

        //  1枚捨てる
        void TrashCard(ICard card);

        //  全てのカードを取り出す
        IList<ICard> Remove();
    }

    public class Trash : ITrash {

        private IList<ICard> Cards { get; set; } = new List<ICard>();

        public IList<ICard> Remove() {
            var clone = new List<ICard>(Cards);
            Cards.Clear();
            return clone;
        }

        public void TrashCard(ICard card) {
            Cards.Add(card);
        }
    }
}
