using Ikkiuchi.Core;
using UnityEngine;
using Zenject;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class MomentContainer : MonoBehaviour{

        public GameObject momentPrefab;
        public bool isMyPlayer;

        public GameObject detailRoot;
        public GameObject draggingContainer;
        public GameObject draggingArrowPrefab;
        public GameObject draggingActionPrefab;

        [Inject] private IRule rule;
        [Inject] private Controller controller;
        [Inject] private PlotViewModel model;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    transform.DestroyAllChildren();

                    for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                        GameObject moment = Instantiate(momentPrefab);

                        moment.GetComponentsInChildren<IControllerSettable>().ForEach(cs => {
                            cs.SetController(controller);
                        });

                        moment.GetComponentsInChildren<IMomentIndexBindable>().ForEach(mib => {
                            if (isMyPlayer) {
                                mib.BindMoment(i, controller.MyPlayer, model);
                            }
                            else {
                                mib.BindMoment(i, controller.EnemyPlayer, model);
                            }
                        });
                        moment.transform.SetParent(transform, false);
                        moment.GetComponent<RectTransform>().SetAsLastSibling();
                    }

                    GetComponentsInChildren<Detail>().ForEach(d => {
                        d.DetailRoot = detailRoot;
                    });

                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(null);
                    });

                    GetComponentsInChildren<DraggableCard>().ForEach(d => {
                        d.DraggingArrowPrefab = draggingArrowPrefab;
                        d.DraggingActionPrefab = draggingActionPrefab;
                        d.DraggingContainer = draggingContainer;
                    });

                })
                .AddTo(this);
        }
    }
}
