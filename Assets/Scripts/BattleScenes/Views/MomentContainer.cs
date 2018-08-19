using Ikkiuchi.Core;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class MomentContainer : MonoBehaviour{

        public GameObject momentPrefab;

        [Inject] private IRule rule;

        private void Start() {
            transform.DestroyAllChildren();

            for(int i = 0; i < rule.CountOfMoment; ++i) {
                GameObject moment = Instantiate(momentPrefab);
                moment.transform.SetParent(transform, false);
                moment.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }
    }
}
