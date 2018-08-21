using Ikkiuchi.Core;
using Zenject;
using UniRx;
using System.Linq;

namespace Ikkiuchi.BattleScenes.ViewModels {
    public class PlotViewModel {
        
        [Inject] private Controller controller;

        public ICard[] MovePlots { get; private set; }

        [Inject]
        public PlotViewModel(IRule rule) {
            rule.CountOfMoment.ToReactiveProperty().Subscribe(cm => {
                MovePlots = new ICard[cm];
            });
        }

        //  移動プロットがペナルティかカードで埋まっている
        public bool IsMovePlotFull() {
            return MovePlots.Select((c, i) => new { Card = c, Index = i })
                .Any(x => !controller.MyPlayer.Plots.IsMovePenarized(x.Index) && x.Card == null);
        }

    }
}
