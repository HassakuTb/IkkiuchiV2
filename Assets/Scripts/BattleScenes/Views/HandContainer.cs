using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Zenject;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class HandContainer : MonoBehaviour {

        public GameObject cardPrefab;
        public GameObject detailRoot;
        public GameObject draggingContainer;

        public GameObject draggingArrowPrefab;
        public GameObject draggingActionPrefab;

        [Inject] private Controller controller;
        [Inject] private PlotViewModel plotModel;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(p => {
                    switch (p) {
                        case Phase.MovePlot:
                        case Phase.ActionPlot:
                            UpdateCards();
                            break;
                    }
                })
                .AddTo(this);
        }

        private void UpdateCards() {
            transform.DestroyAllChildren();

            controller.MyPlayer.Hand.Cards.ForEach(card => {
                GameObject elm = Instantiate(cardPrefab);
                elm.GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                    cb.BindCard(card);
                });
                elm.GetComponentsInChildren<IPlotBindable>().ForEach(pb => {
                    pb.BindPlotModel(plotModel);
                });

                elm.GetComponent<Detail>().DetailRoot = detailRoot;
                elm.GetComponent<DraggableCard>().DraggingContainer = draggingContainer;
                elm.GetComponent<DraggableCard>().DraggingArrowPrefab = draggingArrowPrefab;
                elm.GetComponent<DraggableCard>().DraggingActionPrefab = draggingActionPrefab;
                elm.transform.SetParent(transform, false);
                elm.GetComponent<RectTransform>().SetAsLastSibling();
            });
        }
    }
}
