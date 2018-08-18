using Ikkiuchi.Core;
using UnityEngine;

namespace Ikkiuchi.BattleScenes.Views {
    public static class PosExtensions {

        private const float offset = 60f;
        private const float cellSize = 120f;

        public static Vector3 ToRectTransformPos(this Pos pos, bool isPlayer1) {
            if (isPlayer1) {
                return new Vector3(pos.X * cellSize + offset, pos.Y * cellSize + offset, 0f);
            }
            else {
                float xoffset = cellSize * Board.SizeX - offset;
                float yoffset = cellSize * Board.SizeY - offset;
                return new Vector3(xoffset - pos.X * cellSize, yoffset - pos.Y * cellSize, 0f);
            }
        }
    }
}
