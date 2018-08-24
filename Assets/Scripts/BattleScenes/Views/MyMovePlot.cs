using Ikkiuchi.Core;
using UnityEngine;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class MyMovePlot : MonoBehaviour{

        private Controller controller;

        public void SetContoller(Controller controller) {

            GetComponentsInChildren<DraggableCard>().ForEach(dc => {
                dc.SetAsMovePlot();
            });

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(p => {
                    switch (p) {
                        case Phase.MovePlot:
                            GetComponentsInChildren<DraggableCard>().ForEach(dc => {
                                dc.enabled = true;
                            });
                            break;
                        case Phase.ActionPlot:
                            GetComponentsInChildren<DraggableCard>().ForEach(dc => {
                                dc.enabled = false;
                            });
                            break;
                    }
                })
                .AddTo(this);
        }
    }
}
