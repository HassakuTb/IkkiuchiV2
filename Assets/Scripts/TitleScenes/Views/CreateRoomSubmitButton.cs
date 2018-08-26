using UI;
using UnityEngine;
using UniRx;
using Zenject;
using Ikkiuchi.TitleScenes.ViewModels;
using System;

namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(LongClickFillButton))]
    [RequireComponent(typeof(CanvasGroup))]
    public class CreateRoomSubmitButton : MonoBehaviour{

        [Inject] private CreateRoomModel model;
        [Inject] private WindowState windowState;

        private void Start() {
            GetComponent<LongClickFillButton>().onLongClick.AddListener(() => {

                var prop = new ExitGames.Client.Photon.Hashtable();

                prop.Add("CountOfMoment", model.MomentCount);
                prop.Add("MaxLife", model.MaxLife);
                prop.Add("EnableTrump", model.IsEnabledTrump);
                prop.Add("Seed", Environment.TickCount);

                RoomOptions opt = new RoomOptions();
                opt.IsOpen = true;
                opt.IsVisible = true;
                opt.MaxPlayers = 2;
                opt.CustomRoomProperties = prop;
                
                if (!PhotonNetwork.CreateRoom(model.RoomName, opt, null)){
                    Debug.LogWarning("failed create room");

                }
                windowState.State = WindowStateEnum.WaitJoin;
            });

            CanvasGroup cg = GetComponent<CanvasGroup>();
            this.ObserveEveryValueChanged(_ => model.RoomName)
                .Select(name => name.Trim().Length > 0)
                .Subscribe(enabled => {
                    cg.alpha = enabled ? 1f : 0.3f;
                    cg.blocksRaycasts = enabled;
                })
                .AddTo(this);
        }

        public void OnCreatedRoom() {
            Debug.Log("Room Created");
        }
    }
}
