using Ikkiuchi.Core;
using UnityEngine;
using UniRx;
using Ikkiuchi.BattleScenes.ViewModels;

namespace Ikkiuchi.BattleScenes.Views {
    public class EnemyMovePlot : MonoBehaviour, IMomentIndexBindable{

        public void BindMoment(int momentIndex, IPlayer player, PlotViewModel model) {
            this.ObserveEveryValueChanged(_ => Controller.Instance.CurrentPhase)
                .Where(phase => phase == Phase.ActionPlot)
                .Subscribe(_ => {
                    var c = player.Plots.GetMovePlot(momentIndex);
                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(c);
                    });
                })
                .AddTo(this);
            this.ObserveEveryValueChanged(_ => Controller.Instance.CurrentPhase)
                .Where(phase => phase == Phase.MovePlot)
                .Subscribe(_ => {
                    var c = player.Plots.GetMovePlot(momentIndex);
                    GetComponentsInChildren<ICardBindable>().ForEach(cb => {
                        cb.BindCard(null);
                    });
                })
                .AddTo(this);
        }
    }
}
