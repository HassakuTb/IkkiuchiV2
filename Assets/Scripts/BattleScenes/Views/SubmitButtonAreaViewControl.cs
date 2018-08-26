using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using Zenject;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class SubmitButtonAreaViewControl : MonoBehaviour{

        public GameObject moveSubmitButton;
        public GameObject actionSubmitButton;
        public GameObject waitingText;

        private Controller controller;

        private void Start() {
            controller = Controller.Instance;

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase).Select(_ => Unit.Default)
                .Merge(this.ObserveEveryValueChanged(_ => controller.IsReadyMove(controller.MyPlayer)).Select(_ => Unit.Default))
                .Merge(this.ObserveEveryValueChanged(_ => controller.IsReadyAction(controller.MyPlayer)).Select(_ => Unit.Default))
                .Subscribe(_ => {
                    if(controller.CurrentPhase == Phase.MovePlot) {
                        if (!controller.IsReadyMove(controller.MyPlayer)) {
                            moveSubmitButton.SetActive(true);
                            actionSubmitButton.SetActive(false);
                            waitingText.SetActive(false);
                        }
                        else {
                            moveSubmitButton.SetActive(false);
                            actionSubmitButton.SetActive(false);
                            waitingText.SetActive(true);
                        }
                    }
                    else if(controller.CurrentPhase == Phase.ActionPlot) {
                        if (!controller.IsReadyAction(controller.MyPlayer)) {
                            moveSubmitButton.SetActive(false);
                            actionSubmitButton.SetActive(true);
                            waitingText.SetActive(false);
                        }
                        else {
                            moveSubmitButton.SetActive(false);
                            actionSubmitButton.SetActive(false);
                            waitingText.SetActive(true);
                        }
                    }
                    else {
                        moveSubmitButton.SetActive(false);
                        actionSubmitButton.SetActive(false);
                        waitingText.SetActive(false);
                    }


                })
                .AddTo(this);
        }
    }
}
