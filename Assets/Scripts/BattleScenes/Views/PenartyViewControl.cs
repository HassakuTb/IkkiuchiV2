using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class PenartyViewControl : MonoBehaviour, ICardBindable{

        public GameObject movePenarty;
        public GameObject actionPenarty;

        public void BindCard(ICard card) {
            movePenarty.SetActive(card != null && card.Action.PenartyMove);
            actionPenarty.SetActive(card != null && card.Action.PenartyAction);
        }
    }
}
