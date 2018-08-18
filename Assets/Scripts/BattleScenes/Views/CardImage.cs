using IkkiuchiV2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace IkkiuchiV2.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardImage : MonoBehaviour, ICardBindable {

        public void BindCard(ICard card) {
            GetComponent<Image>().sprite = card.Action.CardImage;
        }
    }
}
