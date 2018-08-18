using IkkiuchiV2.Core;
using UnityEngine;

namespace IkkiuchiV2.BattleScenes.Views {
    public class PenartyViewControl : MonoBehaviour, ICardBindable{

        public GameObject movePenarty;
        public GameObject actionPenarty;

        public void BindCard(ICard card) {
            movePenarty.SetActive(card.Action.PenartyMove);
            actionPenarty.SetActive(card.Action.PenartyAction);
        }
    }
}
