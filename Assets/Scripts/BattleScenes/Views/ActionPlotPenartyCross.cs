using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class ActionPlotPenartyCross : MonoBehaviour, IMomentIndexBindable {

        public GameObject cross;

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {

            this.ObserveEveryValueChanged(_ => player.Plots.IsActionPenarized(momentIndex))
                .Subscribe(penarized => {
                    cross.SetActive(penarized);
                })
                .AddTo(this);

            cross.SetActive(player.Plots.IsActionPenarized(momentIndex));
        }
    }
}
