using Ikkiuchi.TitleScenes.ViewModels;
using Zenject;

namespace Ikkiuchi.TitleScenes {
    public class ViewModelInstaller : MonoInstaller<ViewModelInstaller> {

        public override void InstallBindings() {
            Container.Bind<WindowState>().AsSingle();
            Container.Bind<CreateRoomModel>().AsSingle();
            Container.Bind<JoinRoomModel>().AsSingle();
        }
    }
}
