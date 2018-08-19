using Ikkiuchi.Core;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes {
    public class SceneLoader : MonoBehaviour {

        [Inject] private Controller controller;

        private void Start() {
            controller.MakeBoard();
        }
    }
}
