using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public class MoveUp : MonoBehaviour {

        public float movePerFrame = 0.05f;

        private void Update() {
            transform.localPosition += new Vector3(0, movePerFrame, 0);
        }
    }
}
