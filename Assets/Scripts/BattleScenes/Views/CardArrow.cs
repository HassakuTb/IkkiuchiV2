using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class CardArrow : MonoBehaviour, ICardBindable {

        private float alpha;

        private void Awake() {
            alpha = GetComponent<Image>().color.a;
        }

        public void BindCard(ICard card) {
            Image image = GetComponent<Image>();
            if (card == null) {
                image.color = new Color {
                    r = image.color.r,
                    g = image.color.g,
                    b = image.color.b,
                    a = 0f
                };
            }
            else {
                image.color = new Color {
                    r = image.color.r,
                    g = image.color.g,
                    b = image.color.b,
                    a = alpha
                };

                RectTransform rect = GetComponent<RectTransform>();
                rect.localRotation = Quaternion.Euler(0f, 0f, card.MoveDirection.ToRotateZ());
            }
        }
    }
}
