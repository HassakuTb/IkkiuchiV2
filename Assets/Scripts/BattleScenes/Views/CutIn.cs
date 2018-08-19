using Ikkiuchi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public float slideInTime;
        public float slideOutTime;

        public AnimationCurve slideIn;
        public AnimationCurve slideOut;

        private Text cutInTarget;

        [Inject] private Controller controller;

        private void Start() {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Subscribe(p => {

                })
                .AddTo(this);
        }
    }
}
