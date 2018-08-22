using System;
using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using Zenject;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class CardProtter : MonoBehaviour, ICardBindable , IMomentIndexBindable{

        public bool isAction;

        private PlotViewModel model;

        private int momentIndex;

        public void BindCard(ICard card) {
            if (isAction) {
            }
            else {
                model.PlotMove(card, momentIndex);
            }
        }

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel pvm) {
            this.momentIndex = momentIndex;
            model = pvm;
        }
    }
}
