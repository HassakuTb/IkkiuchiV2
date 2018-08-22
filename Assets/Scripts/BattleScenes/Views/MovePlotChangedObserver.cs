using System;
using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class MovePlotChangedObserver : MonoBehaviour, IMomentIndexBindable {

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {

            model.MovePlots.ObserveReplace()
                .Subscribe(_ => {
                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(model.MovePlots[momentIndex]);
                    });
                })
                .AddTo(this);
        }
    }
}
