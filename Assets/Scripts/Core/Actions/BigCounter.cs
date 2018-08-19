using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ikkiuchi.Core.Actions {
    //  先の先
    [CreateAssetMenu(menuName = "Actions/BigCounter", fileName = "BigCounter")]
    public class BigCounter : Action{

        public override void ResolveCounter(int momentIndex, IPlayer player) {
            player.BigCounter = true;
        }

        public override string GetDetailText() {
            return "この瞬間に受けるはずのダメージを0にして相手に受けるはずの2倍のダメージ";
        }
    }
}
