using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class EnemyMovePlot : MonoBehaviour, IMomentIndexBindable{

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            this.ObserveEveryValueChanged(_ => player.Plots.GetMovePlot(momentIndex))
                .Subscribe(c => {
                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(c);
                    });
                })
                .AddTo(this);
        }
    }
}
