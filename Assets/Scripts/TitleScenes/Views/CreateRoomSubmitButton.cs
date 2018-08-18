using UI;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Ikkiuchi.TitleScenes.ViewModels;

namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(LongClickFillButton))]
    [RequireComponent(typeof(CanvasGroup))]
    public class CreateRoomSubmitButton : MonoBehaviour{

        [Inject] private CreateRoomModel model;
        [Inject] private WindowState windowState;

        private void Start() {
            GetComponent<LongClickFillButton>().onLongClick.AddListener(() => {
                if (!PhotonNetwork.CreateRoom(model.RoomName)){
                    Debug.LogWarning("failed create room");
                }
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
            var prop = new ExitGames.Client.Photon.Hashtable();
            prop.Add("CountOfMoment", model.MomentCount);
            prop.Add("MaxLife", model.MaxLife);
            prop.Add("EnableTrump", model.IsEnabledTrump);

            PhotonNetwork.room.SetCustomProperties(prop);

            windowState.State = WindowStateEnum.WaitJoin;
        }
    }
}
