using Ikkiuchi.Core;
using UnityEngine;
using Zenject;
using System;

namespace Ikkiuchi.BattleScenes {
    public class SceneLoader : MonoBehaviour {

        [Inject] private Controller controller;
        

        private void Start() {
            controller.SetSeed((uint)Environment.TickCount); //  TODO
            controller.MakeBoard(true);
        }
    }
}
