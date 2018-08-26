using UnityEngine;
using UnityEngine.UI;
using Ikkiuchi.Core;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class CardDroppable : MonoBehaviour, IControllerSettable, IMomentIndexBindable{

        public bool isAction;
        private Controller controller;
        private int index;

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            index = momentIndex;
        }

        public void DropCard(ICard card) {
            if ((isAction && controller.CurrentPhase == Phase.ActionPlot) ||
                (!isAction && controller.CurrentPhase == Phase.MovePlot)) {

                if (isAction && controller.MyPlayer.Plots.IsActionPenarized(index)) {
                    return;
                }
                if (!isAction && controller.MyPlayer.Plots.IsMovePenarized(index)) {
                    return;
                }

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
