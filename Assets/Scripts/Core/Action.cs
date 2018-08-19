using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.Core {
    //  解決フェーズで実行する動作の基底
    public interface IAction {

        Controller Controller { get; set; }

        //  アクション名
        string ActionName { get; }

        //  移動ペナルティ
        bool PenartyMove { get; }

        //  行動ペナルティ
        bool PenartyAction { get; }

        //  切り札か？
        bool IsTrump { get; }

        //  カード画像
        Sprite CardImage { get; }

        //  攻撃範囲
        IEnumerable<RelativePos> EnumerateDamageRange();

        //  専攻移動
        Pos ExpectedRapidMove(int momentIndex, IPlayer player);

        //  先制攻撃効果を持っているか
        bool HasRapidAttack();

        //  先制効果を持っているか
        bool HasActionAttack();

        //  カウンター効果を持っているか
        bool HasCounter();

        //  先制攻撃
        int ResolveRapidAttack(int momentIndex, IPlayer player);

        //  カウンター系
        void ResolveCounter(int momentIndex, IPlayer player);

        //  ダメージ処理
        int ResolveDamage(int momentIndex, IPlayer player);

        //  攻撃後移動
        Direction ExpectedActionMove(int momentIndex, IPlayer player);

        Direction ActionMoveDirection { get; }

        //  相手を動かす効果
        Pos ExpectedEnemyMove(int momentIndex, IPlayer player);

        //  射程があるか？
        bool HasRange { get; }

        //  詳細テキストを取得
        string GetDetailText();
    }

    public abstract class Action : ScriptableObject, IAction {

        public Controller Controller { get; set; }

        public string actionName;
        public Sprite cardImage;
        public bool penartyMove;
        public bool penartyAction;
        public bool isTrump;
        public bool hasRange;

        public bool hasRapidAttack;
        public bool hasCounter;
        public bool hasActionAttack;

        public bool HasRapidAttack() {
            return hasRapidAttack;
        }

        public bool HasActionAttack() {
            return hasActionAttack;
        }

        public bool HasCounter() {
            return hasCounter;
        }

        public string ActionName {
            get {
                return actionName;
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

        public bool HasRange {
            get {
                return hasRange;
            }
        }

        public virtual Direction ActionMoveDirection {
            get {
                return Direction.None;
            }
        }

        public virtual string GetDetailText() {
            return "";
        }

        public virtual IEnumerable<RelativePos> EnumerateDamageRange() {
            return Enumerable.Empty<RelativePos>();
        }

        public virtual Pos ExpectedRapidMove(int momentIndex, IPlayer player) {
            return player.Gradiator.Position;
        }

        public virtual int ResolveRapidAttack(int momentIndex, IPlayer player) {
            return 0;
        }

        public virtual void ResolveCounter(int momentIndex, IPlayer player) {
        }

        public virtual int ResolveDamage(int momentIndex, IPlayer player) {
            return 0;
        }

        public virtual Direction ExpectedActionMove(int momentIndex, IPlayer player) {
            return Direction.None;
        }

        public virtual Pos ExpectedEnemyMove(int momentIndex, IPlayer player) {
            if(player == Controller.Player1) {
                return Controller.Player2.Gradiator.Position;
            }
            else {
                return Controller.Player1.Gradiator.Position;
            }
        }
    }
}
