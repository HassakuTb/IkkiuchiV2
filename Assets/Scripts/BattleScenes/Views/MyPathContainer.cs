using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class MyPathContainer : MonoBehaviour{

        [Inject] private IRule rule;
        [Inject] private PlotViewModel model;
        [Inject] private Controller controller;

        public GameObject pathPrefab;

        private List<GameObject> pathes = new List<GameObject>();

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(_ => UpdatePath())
                .AddTo(this);

            model.MovePlots.ObserveReplace()
                .Subscribe(_ => UpdatePath())
                .AddTo(this);

            model.ActionPlots.ObserveReplace()
                .Subscribe(_ => UpdatePath())
                .AddTo(this);

            for(int i = 0; i < rule.CountOfMoment.Value; ++i) {
                GameObject instance = Instantiate(pathPrefab);
                instance.transform.SetParent(transform);
                pathes.Add(instance);
            }

            Clear();
        }

        private void Clear() {
            pathes.ForEach(go => {
                go.SetActive(false);
            });
        }

        private void UpdatePath() {
            if(controller.CurrentPhase != Phase.MovePlot && controller.CurrentPhase != Phase.ActionPlot) {
                Clear();
                return;
            }

            Pos currentPos = controller.MyPlayer.Gradiator.Position;
            for (int i = 0; i < rule.CountOfMoment.Value; ++i) {
                if(model.ActionPlots[i] != null || model.MovePlots[i] == null) {
                    pathes[i].SetActive(false);
                }
                else {
                    pathes[i].SetActive(true);
                    pathes[i].transform.localPosition = currentPos.ToWorldPos();
                    Direction dir = model.MovePlots[i].MoveDirection;
                    pathes[i].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, dir.ToRotateZ()));

                    Pos next = controller.MyPlayer.Gradiator.RelativePosToAbsolute(currentPos, dir.ToRelativePos());
                    currentPos = next.IsInboundBoard() ? next : currentPos;
                }
            }

        }
    }
}
