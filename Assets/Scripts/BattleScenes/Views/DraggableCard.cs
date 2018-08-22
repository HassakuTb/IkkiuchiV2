using System.Collections.Generic;
using System.Linq;
using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ikkiuchi.BattleScenes.Views {
    public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICardBindable {

        private ICard card;
        private GameObject dragging;

        public bool dragToNull;

        public GameObject DraggingContainer { private get; set; }
        public GameObject DraggingArrowPrefab { private get; set; }
        public GameObject DraggingActionPrefab { private get; set; }

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

            var raycastResuts = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResuts);

            ICard dragging = card;
            if (dragToNull) {
                GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                    cb.BindCard(null);
                });
            }

            raycastResuts
                .Select(r => r.gameObject.GetComponent<CardDroppable>())
                .Where(d => d != null)
                .ForEach(d => {
                    d.DropCard(dragging);
                });
            
        }
    }
}
