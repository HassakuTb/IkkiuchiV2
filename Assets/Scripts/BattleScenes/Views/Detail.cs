using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ikkiuchi.BattleScenes.Views {
    public class Detail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ICardBindable {

        public GameObject DetailRoot { private get; set; }

        private ICard card;

        public void BindCard(ICard card) {
            this.card = card;
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
