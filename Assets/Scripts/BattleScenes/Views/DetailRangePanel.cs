using UnityEngine;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(CanvasGroup))]
    public class DetailRangePanel : MonoBehaviour, ICardBindable {

        public void BindCard(ICard card) {
            if(card == null || !card.Action.HasRange) {
                GetComponent<CanvasGroup>().alpha = 0f;
            }
            else {
                GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
    }
}
