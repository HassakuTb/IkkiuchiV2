using Ikkiuchi.Core;
using UnityEngine;
using UniRx;

namespace Ikkiuchi.BattleScenes.Views {
    public class MovePlotPenartyCross : MonoBehaviour, IMomentIndexBindable {

        public GameObject cross;

        public void BindMoment(int momentIndex, IPlayer player) {

            this.ObserveEveryValueChanged(_ => player.Plots.IsMovePenarized(momentIndex))
                .Subscribe(penarized => {
                    cross.SetActive(penarized);
                })
                .AddTo(this);

            cross.SetActive(player.Plots.IsMovePenarized(momentIndex));
        }
    }
}
