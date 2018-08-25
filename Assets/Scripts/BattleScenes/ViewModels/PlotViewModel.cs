using Ikkiuchi.Core;
using Zenject;
using UniRx;
using System.Linq;

namespace Ikkiuchi.BattleScenes.ViewModels {
    public class PlotViewModel {
        
        [Inject] private Controller controller;

        public ReactiveCollection<ICard> MovePlots { get; private set; } = new ReactiveCollection<ICard>();
        public ReactiveCollection<ICard> ActionPlots { get; private set; } = new ReactiveCollection<ICard>();

        [Inject]
        public PlotViewModel(IRule rule) {
            rule.CountOfMoment.ToReactiveProperty().Subscribe(cm => {
                for(int i = 0; i < cm; ++i) {
                    MovePlots.Add(null);
                    ActionPlots.Add(null);
                }
            });
        }

        public void Clear() {
            for(int i = 0; i < MovePlots.Count; ++i) {
                MovePlots[i] = null;
                ActionPlots[i] = null;
            }

        }

        //  移動プロットがペナルティかカードで埋まっている
        public bool IsMovePlotFull() {
            return MovePlots.Select((c, i) => new { Card = c, Index = i })
                .All(x => controller.MyPlayer.Plots.IsMovePenarized(x.Index) || x.Card != null);
        }

        public void PlotMove(ICard card, int index) {
            for (int i = 0; i < MovePlots.Count; ++i) {
                if (MovePlots[i] == card) {
                    MovePlots[i] = null;
                }
            }

            MovePlots[index] = card;
        }

        public void PlotAction (ICard card, int index) {
            for (int i = 0; i < ActionPlots.Count; ++i) {
                if (ActionPlots[i] == card) {
                    ActionPlots[i] = null;
                }
            }

            ActionPlots[index] = card;
        }

    }
}
