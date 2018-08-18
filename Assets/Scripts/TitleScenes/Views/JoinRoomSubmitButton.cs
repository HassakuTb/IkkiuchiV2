using UI;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Ikkiuchi.TitleScenes.ViewModels;

namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class JoinRoomSubmitButton : MonoBehaviour{

        [Inject] private JoinRoomModel model;
        [Inject] private WindowState windowState;

        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                PhotonNetwork.JoinRoom(model.RoomName);
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
    }
}
