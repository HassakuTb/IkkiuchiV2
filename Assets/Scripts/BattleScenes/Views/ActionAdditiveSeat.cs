using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class ActionAdditiveSeat : MonoBehaviour, IMomentIndexBindable, IControllerSettable{

        public GameObject seat;

        private Controller controller;
        private int momentIndex;

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            this.momentIndex = momentIndex;
        }

        public void SetController(Controller controller) {
            this.controller = controller;
        }

        private void Update() {
            if(controller.CurrentPhase != Phase.ActionPlot) {
                seat.SetActive(false);
            }
            else {
                if (controller.MyPlayer.Plots.IsActionPenarized(momentIndex)) {
                    seat.SetActive(false);
                }
                else {
                    seat.SetActive(true);
                }
            }
        }
    }
}
