using IkkiuchiV2.Core;
using UnityEngine;

namespace IkkiuchiV2.BattleScenes.Views {

    [RequireComponent(typeof(RectTransform))]
    public class CardDetailRectTransform : MonoBehaviour, ICardBindable {

        private const float sizeLarge = 320f;
        private const float sizeSmall = 160f;

        public void BindCard(ICard card) {
            RectTransform rect = GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(card.Action.HasRange ? sizeSmall : sizeLarge, rect.sizeDelta.y);
        }
    }
}
