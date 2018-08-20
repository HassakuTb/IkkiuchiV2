using UnityEngine;
using UnityEngine.EventSystems;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {

    public class Card : MonoBehaviour, ICardBindable, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{
        
        private ICard card;
        public GameObject DetailRoot { private get; set; }
        public GameObject DraggingContainer { private get; set; }
        public GameObject DraggingArrowPrefab { private get; set; }
        public GameObject DraggingActionPrefab { private get; set; }

        private GameObject dragging;

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

        public void BindCard(ICard card) {
            this.card = card;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            dragging = Instantiate(DraggingArrowPrefab);
            dragging.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(card);
            });
            dragging.transform.SetParent(DraggingContainer.transform, false);
            dragging.transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            dragging.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            // nop
        }

        public void OnPointerEnter(PointerEventData eventData) {
            DetailRoot.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(card);
            });

            isSelected = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            DetailRoot.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(null);
            });

            isSelected = false;
        }

    }
}
