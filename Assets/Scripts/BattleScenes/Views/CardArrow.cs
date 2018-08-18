using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(RectTransform))]
    public class CardArrow : MonoBehaviour, ICardBindable {

        public void BindCard(ICard card) {
            RectTransform rect = GetComponent<RectTransform>();
            rect.localRotation = Quaternion.Euler(0f, 0f, card.MoveDirection.ToRotateZ());
        }
    }
}
