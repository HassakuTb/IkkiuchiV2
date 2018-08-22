using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    public interface IMomentIndexBindable {

        void BindMoment(int momentIndex, IPlayer player, PlotViewModel model);
    }
}
