using Ikkiuchi.Core;
using Zenject;
using RandomGen;

namespace Ikkiuchi.BattleScenes {
    public class CoreInstaller : MonoInstaller<CoreInstaller> {

        public override void InstallBindings() {
            Container.Bind<Controller>().AsSingle();
            Container.Bind<RandomGenerator>().To<XorShift128>().AsSingle();
        }
    }
}
