using Ikkiuchi.Core.Actions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            private Controller controller;

            public Factory(Controller controller) {
                this.controller = controller;
            }

            public ICardSet CreateClassic() {
                CardSet set = new CardSet();

                ClassicSetDefinition().ForEach(c => {
                    c.Action.Controller = controller;
                    set.cards.Add(c);
                });

                return set;
            }

            private IEnumerable<ICard> ClassicSetDefinition() {
                int i = 0;
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<ThrowStone>("ScriptableObjects/Actions/ThrowStone")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigCut>("ScriptableObjects/Actions/BigCut")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigCut>("ScriptableObjects/Actions/BigCut")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigCut>("ScriptableObjects/Actions/BigCut")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<SaimenPress>("ScriptableObjects/Actions/SaimenPress")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Pierce>("ScriptableObjects/Actions/Pierce")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Pierce>("ScriptableObjects/Actions/Pierce")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Pierce>("ScriptableObjects/Actions/Pierce")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Moonsault>("ScriptableObjects/Actions/Moonsault")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Moonsault>("ScriptableObjects/Actions/Moonsault")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Spear>("ScriptableObjects/Actions/Spear")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Spear>("ScriptableObjects/Actions/Spear")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Spear>("ScriptableObjects/Actions/Spear")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Push>("ScriptableObjects/Actions/Push")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Push>("ScriptableObjects/Actions/Push")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Push>("ScriptableObjects/Actions/Push")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Axe>("ScriptableObjects/Actions/Axe")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Axe>("ScriptableObjects/Actions/Axe")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Axe>("ScriptableObjects/Actions/Axe")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigPierce>("ScriptableObjects/Actions/BigPierce")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigPierce>("ScriptableObjects/Actions/BigPierce")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigPierce>("ScriptableObjects/Actions/BigPierce")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Cotton>("ScriptableObjects/Actions/Cotton")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<MoveCut>("ScriptableObjects/Actions/MoveCut")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<MoveCut>("ScriptableObjects/Actions/MoveCut")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<MoveCut>("ScriptableObjects/Actions/MoveCut")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Snipe>("ScriptableObjects/Actions/Snipe")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Snipe>("ScriptableObjects/Actions/Snipe")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Snipe>("ScriptableObjects/Actions/Snipe")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Rifle>("ScriptableObjects/Actions/Rifle")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Rifle>("ScriptableObjects/Actions/Rifle")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Rifle>("ScriptableObjects/Actions/Rifle")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Rifle>("ScriptableObjects/Actions/Rifle")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Counter>("ScriptableObjects/Actions/Counter")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<ChangeDirection>("ScriptableObjects/Actions/ChangeDirectionFront")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<ChangeDirection>("ScriptableObjects/Actions/ChangeDirectionBack")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<ChangeDirection>("ScriptableObjects/Actions/ChangeDirectionLeft")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<ChangeDirection>("ScriptableObjects/Actions/ChangeDirectionRight")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Javelin>("ScriptableObjects/Actions/Javelin")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Javelin>("ScriptableObjects/Actions/Javelin")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Javelin>("ScriptableObjects/Actions/Javelin")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<SwingAround>("ScriptableObjects/Actions/SwingAround")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<SwingAround>("ScriptableObjects/Actions/SwingAround")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<SwingAround>("ScriptableObjects/Actions/SwingAround")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<SwingAround>("ScriptableObjects/Actions/SwingAround")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<GalBlaster>("ScriptableObjects/Actions/GalBlaster")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Cut>("ScriptableObjects/Actions/Cut")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Cut>("ScriptableObjects/Actions/Cut")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Cut>("ScriptableObjects/Actions/Cut")), Direction.Front);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Iai>("ScriptableObjects/Actions/Issen")), Direction.Back);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<Raid>("ScriptableObjects/Actions/Raid")), Direction.Left);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigCounter>("ScriptableObjects/Actions/BigCounter")), Direction.Right);
                yield return new Card(i++, ScriptableObject.Instantiate(Resources.Load<BigSnipe>("ScriptableObjects/Actions/BigSnipe")), Direction.Front);
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
