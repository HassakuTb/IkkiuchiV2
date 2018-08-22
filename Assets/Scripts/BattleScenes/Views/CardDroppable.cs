using UnityEngine;
using UnityEngine.UI;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardDroppable : MonoBehaviour{

        public void DropCard(ICard card) {

            GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                cb.BindCard(card);
            });
        }
    }
}
