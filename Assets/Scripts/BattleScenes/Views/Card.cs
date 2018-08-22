using UnityEngine;
using UnityEngine.EventSystems;
using Ikkiuchi.Core;
using Ikkiuchi.BattleScenes.ViewModels;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {

    [RequireComponent(typeof(CanvasGroup))]
    public class Card : MonoBehaviour, ICardBindable, IPointerEnterHandler, IPointerExitHandler, IPlotBindable {
        
        private ICard card;

        private const float animationDelta = 15f;
        private float animationTargetH;
        private float animationBaseH;
        private bool isSelected = false;

        private bool isInitialized = false;

        private void Update() {
            if (!isInitialized) {
                animationBaseH = GetComponent<RectTransform>().sizeDelta.y;
                animationTargetH = animationBaseH + animationDelta;

                isInitialized = true;
            }
            
            RectTransform rect = GetComponent<RectTransform>();
            if (isSelected) {
                if(rect.sizeDelta.y < animationTargetH) {
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 3f);
                }
                else {
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, animationTargetH);
                }
            }
            else {
                if (rect.sizeDelta.y > animationBaseH) {
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - 3f);
                }
                else {
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, animationBaseH);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            isSelected = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            isSelected = false;
        }

        public void BindCard(ICard card) {
            this.card = card;
        }

        public void BindPlotModel(PlotViewModel model) {
            model.MovePlots.ObserveReplace()
                .Subscribe(_ => {
                    CanvasGroup cg = GetComponent<CanvasGroup>();
                    if (model.MovePlots.Contains(card)) {
                        cg.alpha = 0.3f;
                        cg.blocksRaycasts = false;
                    }
                    else {
                        cg.alpha = 1f;
                        cg.blocksRaycasts = true;
                    }
                })
                .AddTo(this);
        }
    }
}
