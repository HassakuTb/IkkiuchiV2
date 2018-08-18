using Ikkiuchi.TitleScenes.ViewModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ikkiuchi.TitleScenes.Views {
    [RequireComponent(typeof(Dropdown))]
    public class MaxLifeDropdown : MonoBehaviour{

        [Inject]
        private CreateRoomModel model;

        private List<int> values;

        private void Start() {
            values = Enumerable.Range(1, 10).ToList();

            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.options.Clear();
            dropdown.AddOptions(values.Select(x => x.ToString()).ToList());

            dropdown.onValueChanged.AddListener(index => {
                model.MaxLife = values[index];
            });

            dropdown.value = values.IndexOf(model.MaxLife);
        }
    }
}
