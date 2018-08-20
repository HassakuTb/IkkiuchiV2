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
        }

        public void OnPointerExit(PointerEventData eventData) {
            DetailRoot.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(null);
            });
        }

    }
}
