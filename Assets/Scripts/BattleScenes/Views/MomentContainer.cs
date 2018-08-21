using Ikkiuchi.Core;
using UnityEngine;
using Zenject;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class MomentContainer : MonoBehaviour{

        public GameObject momentPrefab;
        public bool isMyPlayer;

        [Inject] private IRule rule;
        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    transform.DestroyAllChildren();

                    for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                        GameObject moment = Instantiate(momentPrefab);
                        moment.GetComponentsInChildren<IMomentIndexBindable>().ForEach(mib => {
                            if (isMyPlayer) {
                                mib.BindMoment(i, controller.MyPlayer);
                            }
                            else {
                                mib.BindMoment(i, controller.EnemyPlayer);
                            }
                        });
                        moment.transform.SetParent(transform, false);
                        moment.GetComponent<RectTransform>().SetAsLastSibling();
                    }
                })
                .AddTo(this);
        }
    }
}
