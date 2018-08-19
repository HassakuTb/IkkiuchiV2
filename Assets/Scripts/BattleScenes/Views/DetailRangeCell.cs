using System.Linq;
using Ikkiuchi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ikkiuchi.BattleScenes.Views {

    public class DetailRangeCell : MonoBehaviour, ICardBindable{

        public int relativeX;
        public int relativeY;

        public Image damagePanel;
        public Image movePanel;

        public void BindCard(ICard card) {
            if (card == null) return;
            if(card.Action.EnumerateDamageRange().Contains(new RelativePos(relativeX, relativeY))) {
                damagePanel.gameObject.SetActive(true);
            }
            else {
                damagePanel.gameObject.SetActive(false);
            }

            if(card.Action.ActionMoveDirection != Direction.None &&
                card.Action.ActionMoveDirection.ToRelativePos().Equals(new RelativePos(relativeX, relativeY))) {
                movePanel.gameObject.SetActive(true);
            }
            else {
                movePanel.gameObject.SetActive(false);
            }
        }
    }
}
