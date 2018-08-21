using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class EnemyGradiatorIcon : MonoBehaviour{

        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    GetComponent<RectTransform>().anchoredPosition =
                        controller.EnemyPlayer.Gradiator.Position.ToRectTransformPos(controller.EnemyPlayer == controller.Player1);
                })
                .AddTo(this);
        }
    }
}
