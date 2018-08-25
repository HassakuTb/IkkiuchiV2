using Ikkiuchi.Core;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class GradiatorIcon : MonoBehaviour{

        [Inject] private Controller controller;

        public bool isMyPlayer;

        private IPlayer player;

        private const float animationTime = 0.5f;

        private bool isMoveToAnimating;
        private bool isFailedAnimating;
        private Vector3 moveTo;
        private float animationCurrentTime;
        private Vector3 moveToDirection;

        private void Start() {
            player = isMyPlayer ? controller.MyPlayer : controller.EnemyPlayer;

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.Start)
                .Subscribe(_ => {
                    transform.localPosition =
                        player.Gradiator.Position.ToWorldPos();
                })
                .AddTo(this);
        }

        public void StartMoveTo(Pos moveTo) {
            this.moveTo = moveTo.ToWorldPos();
            isMoveToAnimating = true;
            animationCurrentTime = 0f;

            moveToDirection = (this.moveTo - transform.position).normalized;

        }

        public void StartMoveFailed(Pos moveTo) {
            this.moveTo = moveTo.ToWorldPos();
            isFailedAnimating = true;
            animationCurrentTime = 0f;

            moveToDirection = (this.moveTo - transform.position).normalized;

        }

        private void Update() {
            if (isMoveToAnimating) {
                if(animationCurrentTime >= animationTime) {
                    transform.position = player.Gradiator.Position.ToWorldPos();
                    isMoveToAnimating = false;
                }
                else {
                    transform.position += moveToDirection * (Time.deltaTime / animationTime);
                    animationCurrentTime += Time.deltaTime;
                }
            }
            else if(isFailedAnimating){
                if (animationCurrentTime >= animationTime) {
                    transform.position = player.Gradiator.Position.ToWorldPos();
                    isFailedAnimating = false;
                }
                else {
                    Vector3 direction = (animationCurrentTime >= animationTime / 2) ? -moveToDirection : moveToDirection;
                    transform.position += direction * (Time.deltaTime / animationTime);
                    animationCurrentTime += Time.deltaTime;
                }
            }
        }
    }
}
