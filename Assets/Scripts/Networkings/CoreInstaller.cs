using Ikkiuchi.Core;
using Zenject;

namespace Ikkiuchi.Networkings {
    public class CoreInstaller : MonoInstaller<CoreInstaller> {

        public override void InstallBindings() {
            Container.Bind<IRule>().To<Rule>().AsSingle();
        }
    }
}
