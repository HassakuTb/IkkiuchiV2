using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class MyGradiatorIcon : MonoBehaviour{

        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    GetComponent<RectTransform>().anchoredPosition =
                        controller.MyPlayer.Gradiator.Position.ToRectTransformPos(controller.MyPlayer == controller.Player1);
                })
                .AddTo(this);
        }
    }
}
