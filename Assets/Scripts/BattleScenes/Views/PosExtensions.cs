using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public static class PosExtensions {

        public static Vector3 ToWorldPos(this Pos pos, bool isPlayer1) {
            if (isPlayer1) return new Vector3(pos.X, pos.Y, 0f);
            else return new Vector3(Board.SizeX - pos.X - 1, Board.SizeY - pos.Y - 1);

        }
    }
}
