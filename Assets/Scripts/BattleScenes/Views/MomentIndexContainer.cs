using Ikkiuchi.Core;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class MomentIndexContainer : MonoBehaviour{

        public Text textPrefab;

        [Inject] private IRule rule;
        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    transform.DestroyAllChildren();

                    for (int i = 1; i <= rule.CountOfMoment.Value; ++i) {
                        Text text = Instantiate(textPrefab);
                        text.text = i.ToString();
                        text.transform.SetParent(transform, false);
                        text.GetComponent<RectTransform>().SetAsLastSibling();
                    }
                })
                .AddTo(this);
        }
    }
}
