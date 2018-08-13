using UnityEngine;

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

        //  カード画像
        Sprite CardImage { get; }
    }

    public abstract class Action : ScriptableObject, IAction {

        public string actionName;
        public Sprite cardImage;
        public int resolveOrder;
        public bool penartyMove;
        public bool penartyAction;
        
        public string ActionName {
            get {
                return actionName;
            }
        }

        public int ResolveOrder {
            get {
                return resolveOrder;
            }
        }

        public bool PenartyMove {
            get {
                return penartyMove;
            }
        }
        
        public bool PenartyAction {
            get {
                return penartyAction;
            }
        }

        public Sprite CardImage {
            get {
                return cardImage;
            }
        }
    }
}
