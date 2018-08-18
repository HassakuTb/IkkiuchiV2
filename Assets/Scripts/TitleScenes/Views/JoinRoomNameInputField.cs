using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(InputField))]
    public class JoinRoomNameInputField : MonoBehaviour{

        [Inject]
        private JoinRoomModel model;

        private void Start() {
            InputField input = GetComponent<InputField>();

            input.onValueChanged.AddListener(str => {
                model.RoomName = str;
            });

            input.text = model.RoomName;
        }
    }
}
