using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class DraggingCard : MonoBehaviour, ICardBindable {

        public ICard Card { get; private set; }

        public void BindCard(ICard card) {
            Card = card;
        }
    }
}
