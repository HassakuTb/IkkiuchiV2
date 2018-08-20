using Ikkiuchi.Core;
using RandomGen;
using Zenject;

namespace Ikkiuchi.Networkings {
    public class CoreInstaller : MonoInstaller<CoreInstaller> {

        public override void InstallBindings() {
            Container.Bind<IRule>().To<Rule>().AsSingle();
            Container.Bind<Controller>().AsSingle();
            Container.Bind<RandomGenerator>().To<XorShift128>().AsSingle();
        }
    }
}
