using System;
using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardBackground : MonoBehaviour, ICardBindable {

        private float alpha;

        private void Awake() {
            Image image = GetComponent<Image>();
            alpha = image.color.a;
            image.color = new Color {
                r = image.color.r,
                g = image.color.g,
                b = image.color.b,
                a = 0f
            };
        }

        public void BindCard(ICard card) {
            Image image = GetComponent<Image>();
            image.color = new Color {
                r = image.color.r,
                g = image.color.g,
                b = image.color.b,
                a = card == null ? 0f : alpha
            };
        }
    }
}
