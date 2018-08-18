using Zenject;

namespace Ikkiuchi.Networkings {
    public class NetworkingInstaller : MonoInstaller<NetworkingInstaller> {

        public override void InstallBindings() {
            Container.Bind<NetworkingModel>().AsSingle();
        }
    }
}
