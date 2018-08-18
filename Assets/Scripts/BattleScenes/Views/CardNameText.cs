﻿using IkkiuchiV2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace IkkiuchiV2.BattleScenes.Views {
    [RequireComponent(typeof(Text))]
    public class CardNameText : MonoBehaviour, ICardBindable {

        public void BindCard(ICard card) {
            GetComponent<Text>().text = card.Action.ActionName;
        }
    }
}
