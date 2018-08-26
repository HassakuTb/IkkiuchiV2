using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class CutIn : MonoBehaviour{

        public Text start;
        public Text move;
        public Text action;
        public Text resolve;
        public GameObject panel;
        public ResolveInvoker resolveInvoker;

        public float slideTime;

        public AnimationCurve slideS;

        private const float startX = 1500f;
        private const float endX = -1500f;

        private Text target;

        private float currentTime = 0;
        private bool isAnimating;

        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start || p == Phase.MovePlot || p == Phase.ActionPlot || p == Phase.Resolve)
                .Subscribe(p => {
                    switch (p) {
                        case Phase.Start:
                            target = start;
                            StartAnimation();
                            break;
                        case Phase.MovePlot:
                            target = move;
                            StartAnimation();
                            break;
                        case Phase.ActionPlot:
                            target = action;
                            StartAnimation();
                            break;
                        case Phase.Resolve:
                            target = resolve;
                            StartAnimation();
                            break;
                    }
                })
                .AddTo(this);
        }

        private void StartAnimation() {
            RectTransform rect = target.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(startX, rect.anchoredPosition.y);
            currentTime = 0;
            isAnimating = true;
            panel.SetActive(true);
            start.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            action.gameObject.SetActive(false);
            resolve.gameObject.SetActive(false);
            target.gameObject.SetActive(true);
        }

        private void Update() {
            if (!isAnimating) return;
            currentTime += Time.deltaTime;

            float ratio = currentTime / slideTime;
            float x = startX + slideS.Evaluate(ratio) * (endX - startX);
            RectTransform rect = target.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);

            if(currentTime > slideTime) {
                panel.SetActive(false);
                isAnimating = false;

                //  開始処理
                if(target == start) {
                    controller.DealCards();
                }

                else if (target == resolve) {
                    controller.CurrentIndex = 0;
                    resolveInvoker.StartResolve();
                }
            }
        }
    }
}
