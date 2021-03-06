﻿using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {

    [RequireComponent(typeof(Text))]
    public class CardDetailText : MonoBehaviour, ICardBindable {

        public void BindCard(ICard card) {
            if (card == null) GetComponent<Text>().text = "";
            else GetComponent<Text>().text = card.Action.GetDetailText();
        }
    }
}
