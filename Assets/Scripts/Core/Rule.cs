using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikkiuchi.Core {
    //  ルール設定
    public interface IRule {

        //  最大ライフ
        int MaxLife { get; }

        //  最大ターン数
        int MaxTurn { get; }

        //  1ターンに使うカード枚数
        int CountOfMoment { get; }

        //  切り札有りか？
        bool IsEnableTrump { get; }
    }
}
