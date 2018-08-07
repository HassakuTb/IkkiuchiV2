using System.Collections.Generic;
using System.Linq;

namespace IkkiuchiV2.Core {
    /// <summary>
    /// プレイヤーの駒 戦士
    /// </summary>
    public interface IGradiator {

        //  絶対位置
        Pos Positon { get; }

        //  向き
        GradiatorDirection Direction { get; }

        //  相対位置→絶対位置変換
        Pos RelativePosToAbsolute(RelativePos relative);
        
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
