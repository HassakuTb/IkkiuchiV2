using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardDroppable : MonoBehaviour, IDropHandler {

        public void OnDrop(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            DraggingCard dragging = eventData.pointerDrag.GetComponent<DraggingCard>();
            GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(dragging.Card);
            });
        }
    }
}
