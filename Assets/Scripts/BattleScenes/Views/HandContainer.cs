using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class HandContainer : MonoBehaviour {

        public GameObject cardPrefab;
        public GameObject detailRoot;
        
        [Inject] private Controller controller;

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
                elm.GetComponent<Card>().DetailRoot = detailRoot;
                elm.transform.SetParent(transform, false);
                elm.GetComponent<RectTransform>().SetAsLastSibling();
            });
        }
    }
}
