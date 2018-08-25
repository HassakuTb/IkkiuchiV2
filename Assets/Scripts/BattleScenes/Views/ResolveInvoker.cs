using Ikkiuchi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class ResolveInvoker : MonoBehaviour{

        [Inject] private Controller controller;

        private void Update() {
            if (controller.CurrentPhase != Phase.Resolve) return;


        }
    }
}
