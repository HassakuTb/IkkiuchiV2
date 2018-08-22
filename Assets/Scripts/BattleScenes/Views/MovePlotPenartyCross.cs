using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class MovePlotPenartyCross : MonoBehaviour, IMomentIndexBindable {

        public GameObject cross;

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {

            this.ObserveEveryValueChanged(_ => player.Plots.IsMovePenarized(momentIndex))
                .Subscribe(penarized => {
                    cross.SetActive(penarized);
                })
                .AddTo(this);

            cross.SetActive(player.Plots.IsMovePenarized(momentIndex));
        }
    }
}
