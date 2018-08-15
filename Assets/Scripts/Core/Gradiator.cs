using System.Collections.Generic;
using System.Linq;

namespace IkkiuchiV2.Core {
    /// <summary>
    /// プレイヤーの駒 戦士
    /// </summary>
    public interface IGradiator {

        //  絶対位置
        Pos Position { get; set; }

        //  相対位置→絶対位置変換
        Pos RelativePosToAbsolute(RelativePos relative);
    }

    //  実装
    public class Gradiator : IGradiator {

        //  絶対位置
        public Pos Position { get; set; }

        //  向き(consistant)
        private GradiatorDirection Direction { get; set; } 

        /// <summary>
        /// インスタンス初期化
        /// </summary>
        /// <param name="initPos">初期位置</param>
        /// <param name="direction">駒の向き Player1ならNorth, Playwe2ならSouth</param>
        public Gradiator(Pos initPos, GradiatorDirection direction, IPlayer owner) {
            Position = initPos;
            Direction = direction;
        }

        //  相対位置→絶対位置変換
        public Pos RelativePosToAbsolute(RelativePos relative) {
            //  南向きプレイヤー(P2)は相対位置を逆転させて加算する
            switch (Direction) {
                case GradiatorDirection.North:
                    return new Pos(Position.X + relative.X, Position.Y + relative.Y);
                case GradiatorDirection.South:
                    return new Pos(Position.X - relative.X, Position.Y - relative.Y);
                default:
                    UnityEngine.Assertions.Assert.IsTrue(false);
                    return Position;
            }
        }
    }

    //  IEnumerable<RelativePos>拡張
    public static class RelativeCollectionExtensions {

        /// <summary>
        /// IEnumerable<RelativePos>→IEnumerable<Pos>変換
        /// </summary>
        /// <param name="relatives"></param>
        /// <param name="gradiator"></param>
        /// <returns></returns>
        public static IEnumerable<Pos> ToAbsolute(this IEnumerable<RelativePos> relatives, IGradiator gradiator) {
            return relatives.Select(relative => gradiator.RelativePosToAbsolute(relative));
        }
    }
}
