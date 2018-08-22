﻿using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class EnemyGradiatorIcon : MonoBehaviour{

        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    transform.position =
                        controller.EnemyPlayer.Gradiator.Position.ToWorldPos();
                })
                .AddTo(this);
        }
    }
}
