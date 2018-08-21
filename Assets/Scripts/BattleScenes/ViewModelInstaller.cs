using Ikkiuchi.BattleScenes.ViewModels;
using Zenject;

namespace Ikkiuchi.BattleScenes {
    public class ViewModelInstaller : MonoInstaller<ViewModelInstaller> {

        public override void InstallBindings() {
            Container.Bind<PlotViewModel>().AsSingle();
        }
    }
}
