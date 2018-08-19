using Ikkiuchi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    public class MomentIndexContainer : MonoBehaviour{

        public Text textPrefab;

        [Inject] private IRule rule;

        private void Start() {
            transform.DestroyAllChildren();

            for(int i = 1; i <= rule.CountOfMoment; ++i) {
                Text text = Instantiate(textPrefab);
                text.text = i.ToString();
                text.transform.SetParent(transform, false);
                text.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }
    }
}
