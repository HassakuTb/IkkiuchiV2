namespace Ikkiuchi.Core {
    //  ルール設定
    public interface IRule {

        //  最大ライフ
        int MaxLife { get; set; }

        //  1ターンに使うカード枚数
        int CountOfMoment { get; set; }

        //  切り札有りか？
        bool IsEnableTrump { get; set; }
    }

    public class Rule : IRule{
        public int MaxLife { get; set; } = 8;
        public int CountOfMoment { get; set; } = 3;
        public bool IsEnableTrump { get; set; } = true;
    }
}
