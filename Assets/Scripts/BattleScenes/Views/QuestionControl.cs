using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {
    public class QuestionControl : MonoBehaviour, IMomentIndexBindable, IControllerSettable{

        public GameObject question;
        public GameObject[] nonQuestions;

        public GameObject questionText;
        public GameObject actualImage;

        private Controller controller;
        private int index;

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            this.index = momentIndex;
        }

        public void SetController(Controller controller) {
            this.controller = controller;
        }

        private void Update() {
            if(controller.CurrentPhase == Phase.Resolve) {
                nonQuestions.ForEach(go => {
                    go.SetActive(false);
                });
                question.SetActive(true);

                if(controller.CurrentIndex >= index) {
                    actualImage.SetActive(true);
                    questionText.SetActive(false);
                }
                else {
                    actualImage.SetActive(false);
                    questionText.SetActive(true);
                }

            }
            else {
                nonQuestions.ForEach(go => {
                    go.SetActive(true);
                });
                question.SetActive(false);
            }
        }
    }
}
