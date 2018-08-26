using Ikkiuchi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {
    public class SettleControl : MonoBehaviour {

        private Controller controller;

        public GameObject window;
        public Text settleText;

        private void Start() {
            controller = Controller.Instance;

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(phase => {
                    if(phase == Phase.Settle) {
                        window.SetActive(true);
                        if(controller.MyPlayer.Life.IsDead && controller.EnemyPlayer.Life.IsDead) {
                            settleText.text = "引き分け";
                        }
                        else if (controller.MyPlayer.Life.IsDead) {
                            settleText.text = "あなたの負け";
                        }
                        else {
                            settleText.text = "あなたの勝ち";
                        }
                    }
                    else {
                        window.SetActive(false);
                    }

                })
                .AddTo(this);
            window.SetActive(false);
        }
    }
}
