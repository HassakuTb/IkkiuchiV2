using UnityEngine;
using UnityEngine.UI;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardDroppable : MonoBehaviour, IControllerSettable{

        public bool isAction;
        private Controller controller;

        public void DropCard(ICard card) {
            if ((isAction && controller.CurrentPhase == Phase.ActionPlot) ||
                (!isAction && controller.CurrentPhase == Phase.MovePlot)) {

                GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                    cb.BindCard(card);
                });
            }
        }

        public void SetController(Controller controller) {
            this.controller = controller;
        }
    }
}
