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

        //  切り札か？
        bool IsTrump { get; }

        //  カード画像
        Sprite CardImage { get; }

        //  移動先
        RelativePos GetMoveTo();

        //  処理
        void Resolve(bool isCollideMove);
    }

    public abstract class Action : ScriptableObject, IAction {

        public string actionName;
        public Sprite cardImage;
        public int resolveOrder;
        public bool penartyMove;
        public bool penartyAction;
        public bool isTrump;
        
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

        public bool IsTrump {
            get {
                return isTrump;
            }
        }

        public virtual RelativePos GetMoveTo() {
            return new RelativePos(0, 0);
        }

        public　virtual void Resolve(bool isCollideMove) {
            //  nop
        }
    }
}
