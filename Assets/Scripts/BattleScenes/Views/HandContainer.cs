using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Zenject;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(CanvasGroup))]
    public class HandContainer : MonoBehaviour {

        public GameObject cardPrefab;
        public GameObject detailRoot;
        public GameObject draggingContainer;

        public GameObject draggingArrowPrefab;
        public GameObject draggingActionPrefab;

        private Controller controller;
        private PlotViewModel plotModel;

        private void Start() {
            controller = Controller.Instance;
            plotModel = PlotViewModel.Instance;

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(p => {
                    CanvasGroup cg = GetComponent<CanvasGroup>();
                    switch (p) {
                        case Phase.MovePlot:
                            cg.alpha = 1.0f;
                            cg.blocksRaycasts = true;
                            UpdateCards();
                            GetComponentsInChildren<DraggableCard>().ForEach(dc => {
                                dc.SetAsMovePlot();
                            });
                            break;
                        case Phase.ActionPlot:
                            UpdateCards();
                            GetComponentsInChildren<DraggableCard>().ForEach(dc => {
                                dc.SetAsActionPlot();
                            });
                            break;
                        case Phase.Resolve:
                            cg.alpha = 0.3f;
                            cg.blocksRaycasts = false;
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
