using System.Linq;
using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(Image))]
    public class Actual : MonoBehaviour, IMomentIndexBindable , IControllerSettable{

        public Image cardImage;
        public Sprite arrowSprite;

        private Controller controller;
        
        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            this.ObserveEveryValueChanged(_ => controller.CurrentPhase)
                .Where(phase => phase == Phase.Resolve)
                .Subscribe(_ => {
                    if(controller.EnemyPlayer.Plots.GetActionPlot(momentIndex) != null) {
                        GetComponent<Image>().enabled = true;
                        cardImage.sprite = controller.EnemyPlayer.Plots.GetActionPlot(momentIndex).Action.CardImage;
                        cardImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        cardImage.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    else if(controller.EnemyPlayer.Plots.GetMovePlot(momentIndex) != null) {
                        GetComponent<Image>().enabled = false;
                        cardImage.sprite = arrowSprite;
                        cardImage.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, controller.EnemyPlayer.Plots.GetMovePlot(momentIndex).MoveDirection.ToRotateZ()));
                        cardImage.transform.localScale = new Vector3(-0.75f, -0.75f, 1f);
                    }
                    else {
                        GetComponent<Image>().enabled = true;
                        cardImage.sprite = null;
                        cardImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        cardImage.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                })
                .AddTo(this);
        }

        public void SetController(Controller controller) {
            this.controller = controller;
        }
    }
}
