using UnityEngine;
using Zenject;
using Ikkiuchi.Core;

namespace Ikkiuchi.BattleScenes.Views {
    public class EnemyLifeContainer : MonoBehaviour{

        public GameObject lifeItemPrefab;

        private int prevValue = 0;

        private void Update() {
            var controller = Controller.Instance;
            if (controller.EnemyPlayer == null) return;

            if(controller.EnemyPlayer.Life.Value != prevValue) {
                UpdateLife(controller.EnemyPlayer.Life.Value);

                prevValue = controller.EnemyPlayer.Life.Value;
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
