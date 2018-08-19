using UnityEngine;
using UnityEngine.EventSystems;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {

    public class Card : MonoBehaviour, ICardBindable, IPointerEnterHandler, IPointerExitHandler{
        
        private ICard card;
        public GameObject DetailRoot { private get; set; }
        

        public void BindCard(ICard card) {
            this.card = card;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            Debug.Log("Card#OnPointerEnter");
            DetailRoot.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(card);
            });
        }

        public void OnPointerExit(PointerEventData eventData) {
            Debug.Log("Card#OnPointerExit");
            DetailRoot.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(null);
            });
        }
    }
}
