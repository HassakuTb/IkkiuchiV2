using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class ResolveFocus : MonoBehaviour {

        private Controller controller;
        private IRule rule;

        private const float areaWidth = 520f;

        private void Start() {
            controller = Controller.Instance;
            rule = Rule.Instance;
            
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(phase => {
                    if (phase == Phase.Resolve) {
                        GetComponent<Image>().enabled = true;
                    }
                    else {
                        GetComponent<Image>().enabled = false;
                    }
                })
                .AddTo(this);

            this.ObserveEveryValueChanged(_ => controller.CurrentIndex)
                .Subscribe(index => {
                    if (index < 0) index = 0;
                    if (index >= rule.CountOfMoment.Value) index = rule.CountOfMoment.Value - 1;
                    RectTransform rect = GetComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(
                         areaWidth / rule.CountOfMoment.Value,
                         rect.sizeDelta.y
                        );

                    rect.anchoredPosition = new Vector2(
                         areaWidth / rule.CountOfMoment.Value * index,
                         rect.anchoredPosition.y
                         );
                })
                .AddTo(this);
        }
    }
}
