using Ikkiuchi.TitleScenes.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(InputField))]
    public class RoomNameInputField : MonoBehaviour{

        [Inject]
        private CreateRoomModel model;

        private void Start() {
            InputField input = GetComponent<InputField>();

            input.onValueChanged.AddListener(str => {
                model.RoomName = str;
            });

            input.text = model.RoomName;
        }
    }
}
