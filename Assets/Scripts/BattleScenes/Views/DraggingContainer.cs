using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class DraggingContainer : MonoBehaviour{

        private void Update() {
            if (Input.GetMouseButtonUp(0)) {
                transform.DestroyAllChildren();
            }
        }
    }
}
