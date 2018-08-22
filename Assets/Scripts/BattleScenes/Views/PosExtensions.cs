using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public static class PosExtensions {

        public static Vector3 ToWorldPos(this Pos pos) {
            return new Vector3(pos.X, pos.Y, 0f);
        }
    }
}
