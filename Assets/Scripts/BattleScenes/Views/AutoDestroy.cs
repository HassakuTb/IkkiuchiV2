using System;
using UnityEngine;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class AutoDestroy : MonoBehaviour{

        public float time;

        private void Start() {
            Observable.Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
