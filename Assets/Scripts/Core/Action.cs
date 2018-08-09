

namespace IkkiuchiV2.Core {
    //  解決フェーズで実行する動作の基底
    public interface IAction {

        //  アクション名
        string ActionName { get; }

        //  解決優先度
        //  値が小さい方を先に処理する
        int ResolveOrder { get; }

        //  移動ペナルティ
        bool PenartyMove { get; }

        //  行動ペナルティ
        bool PenartyAction { get; }
    }

    public class Action {
    }
}
