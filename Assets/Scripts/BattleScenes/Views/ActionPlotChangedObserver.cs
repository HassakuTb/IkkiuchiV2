using System;
using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class ActionPlotChangedObserver : MonoBehaviour, IMomentIndexBindable {

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {

            model.ActionPlots.ObserveReplace()
                .Subscribe(_ => {
                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(model.ActionPlots[momentIndex]);
                    });
                })
                .AddTo(this);
        }
    }
}
