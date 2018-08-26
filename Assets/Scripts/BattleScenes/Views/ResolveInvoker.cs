using Ikkiuchi.Core;
using System;
using System.Collections;
using UnityEngine;
using Zenject;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class ResolveInvoker : MonoBehaviour{

        [Inject] private Controller controller;
        [Inject] private PlotViewModel plotModel;

        public GradiatorIcon myGrad;
        public GradiatorIcon enemyGrad;

        public MomentContainer[] moments;

        public SpriteRenderer damageValuePrefab;
        public Sprite[] damageSprites;

        public GameObject slash;
        public GameObject heal;
        public GameObject powUp;

        private GradiatorIcon p1Grad;
        private GradiatorIcon p2Grad;

        private bool isResolving;

        private void Start() {
            p1Grad = controller.MyPlayer == controller.Player1 ? myGrad : enemyGrad;
            p2Grad = p1Grad == myGrad ? enemyGrad : myGrad;

            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(p => p == Phase.MovePlot)
                .Subscribe(_ => {
                    plotModel.Clear();
                    moments.ForEach(mc => {
                        mc.GetComponentsInChildren<Detail>().ForEach(d => {
                            d.BindCard(null);
                        }); 
                    });
                })
                .AddTo(this);
        }

        public void SlashEffect(Pos pos) {
            var dam = Instantiate(slash);
            dam.transform.position = pos.ToWorldPos();
        }

        public void HealEffect(Pos pos) {
            var dam = Instantiate(heal);
            dam.transform.position = pos.ToWorldPos();
        }

        public void PowUpEffect(Pos pos) {
            var dam = Instantiate(powUp);
            dam.transform.position = pos.ToWorldPos();
        }

        public void DamageEffect(Pos pos, int damage) {
            var dam = Instantiate(damageValuePrefab);
            dam.transform.position = pos.ToWorldPos();
            dam.sprite = damageSprites[damage];
        }

        public void StartResolve() {
            isResolving = true;
        }

        private void Update() {
            if (!isResolving) return;

            if (controller.CurrentPhase != Phase.Resolve) {
                isResolving = false;
            }

            if (controller.IsAnimationOver) {
                controller.Resolve();

                bool startAnim = false;

                if (controller.P1MoveAnimation) {
                    p1Grad.StartMoveTo(controller.P1MoveTo);
                    startAnim = true;
                }else if (controller.P1MoveFailAnimation) {
                    p1Grad.StartMoveFailed(controller.P1MoveTo);
                    startAnim = true;
                }

                if (controller.P2MoveAnimation) {
                    p2Grad.StartMoveTo(controller.P2MoveTo);
                    startAnim = true;
                }else if (controller.P2MoveFailAnimation) {
                    p2Grad.StartMoveFailed(controller.P2MoveTo);
                    startAnim = true;
                }

                controller.DamageParams.ForEach(dp => {
                    DamageEffect(dp.Pos, dp.Value);
                    startAnim = true;
                });

                controller.Slashes.ForEach(dp => {
                    SlashEffect(dp);
                    startAnim = true;
                });

                controller.PowUp.ForEach(dp => {
                    PowUpEffect(dp);
                    startAnim = true;
                });

                controller.Heal.ForEach(dp => {
                    HealEffect(dp);
                    startAnim = true;
                });

                if (startAnim) {
                    SingleAssignmentDisposable disposable = new SingleAssignmentDisposable();
                    disposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ => {
                        controller.ResetAnimation();
                        disposable.Dispose();
                    });
                }
                

            }            
        }
    }
}
