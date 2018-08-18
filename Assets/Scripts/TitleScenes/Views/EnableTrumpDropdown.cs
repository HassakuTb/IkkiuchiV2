using Ikkiuchi.TitleScenes.ViewModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(Dropdown))]
    public class EnableTrumpDropdown : MonoBehaviour{

        [Inject]
        private CreateRoomModel model;

        private List<bool> values;

        private void Start() {
            values = new List<bool> {
                true,
                false
            };

            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.options.Clear();
            dropdown.AddOptions(values.Select(x => x.ToAriNashi()).ToList());

            dropdown.onValueChanged.AddListener(index => {
                model.IsEnabledTrump = values[index];
            });

            dropdown.value = values.IndexOf(model.IsEnabledTrump);
        }
    }

    public static partial class BoolExtensions {

        public static string ToAriNashi(this bool b) {
            return b ? "あり" : "なし";
        }
    }
}
