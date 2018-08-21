using Ikkiuchi.Core;
using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ikkiuchi.TitleScenes {
    public class BattleSceneLoader : MonoBehaviour{

        [Inject] private WindowState windowState;
        [Inject] private IRule rule;
        [Inject] private Controller controller;

        public void OnJoinedRoom() {
            Debug.Log("OnJoined");
            if (PhotonNetwork.playerList.Length == 2) {
                windowState.State = WindowStateEnum.None;

                var prop = PhotonNetwork.room.CustomProperties;
                rule.CountOfMoment.Value = (int)prop["CountOfMoment"];
                rule.IsEnableTrump = (bool)prop["EnableTrump"];
                rule.MaxLife = (int)prop["MaxLife"];
                controller.SetSeed((uint)prop["Seed"]);

                SceneManager.LoadScene("BattleScene");
            }
        }

        public void OnPhotonPlayerConnected() {
            Debug.Log("OnPlayerConnected");
            if (PhotonNetwork.playerList.Length == 2) {
                windowState.State = WindowStateEnum.None;

                var prop = PhotonNetwork.room.CustomProperties;
                rule.CountOfMoment.Value = (int)prop["CountOfMoment"];
                rule.IsEnableTrump = (bool)prop["EnableTrump"];
                rule.MaxLife = (int)prop["MaxLife"];
                controller.SetSeed((uint)prop["Seed"]);

                SceneManager.LoadScene("BattleScene");
            }
        }
    }
}
