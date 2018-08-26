using UnityEngine;
using Zenject;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    public class MyLifeContainer : MonoBehaviour{

        public GameObject lifeItemPrefab;

        private Controller controller;

        private int prevValue = 0;

        private void Start() {
            controller = Controller.Instance;
        }

        private void Update() {

            if (controller.MyPlayer == null) return;

            if(controller.MyPlayer.Life.Value != prevValue) {
                UpdateLife(controller.MyPlayer.Life.Value);

                prevValue = controller.MyPlayer.Life.Value;
            }
        }

        private void UpdateLife(int lifeValue) {
            transform.DestroyAllChildren();

            for (int i = 0; i < lifeValue; ++i) {
                GameObject item = Instantiate(lifeItemPrefab);
                item.transform.SetParent(transform, false);
                item.GetComponent<RectTransform>().SetAsLastSibling();
            }
            
        }
    }
}
