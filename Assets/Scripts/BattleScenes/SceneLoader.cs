using Ikkiuchi.Core;
using UnityEngine;
using Zenject;
using System;

namespace Ikkiuchi.BattleScenes {
    public class SceneLoader : MonoBehaviour {

        private void Start() {
            Controller.Instance.MakeBoard(PhotonNetwork.isMasterClient);
        }
    }
}
