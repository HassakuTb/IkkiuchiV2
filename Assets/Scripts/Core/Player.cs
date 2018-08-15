﻿using System.Collections.Generic;
using System;

namespace IkkiuchiV2.Core {
    //  ゲームルールとしてのプレイヤー
    public interface IPlayer {

        //  駒への参照
        IGradiator Gradiator { get; }

        //  ライフ
        ILife Life { get; }

        //  手札
        IHand Hand { get; }

        //  プロット
        IPlots Plots { get; }

        //  切り札を必要枚数ドロー
        void DrawTrump(IDeck deck);
        
        //  指定位置に移動しようとする(衝突解決済み)
        void MoveTo(Pos to);

        //  上書き後の実際の行動リストを取得
        IList<IAction> GetActualActions();
    }

    public class Player : IPlayer {

        //  引いた切り札の数
        private int drawedTramps = 0;

        //  切り札を必要枚数ドロー
        public void DrawTrump(IDeck deck) {
            if(Life.Value <= Life.Max / 2) {
                if(drawedTramps < 1) {
                    Hand.Cards.Add(deck.DealTrump());
                    drawedTramps++;
                }
            }
            if(Life.Value <= Life.Max / 4) {
                if(drawedTramps < 2) {
                    Hand.Cards.Add(deck.DealTrump());
                    drawedTramps++;
                }
            }
        }

        public void MoveTo(Pos to) {
            if (!to.IsInboundBoard()) {
                //  マップ外なら1ダメージを受けて終了
                Life.DealDamage(1);
                //  TODO:   アニメーション
            }
            else {
                Gradiator.Position = to;
                //  TODO:   アニメーション
            }

        }

        public Player(bool isPlayer1, IRule rule) {
            Life = new Life(rule.MaxLife);
            Pos initialPos = new Pos(Board.SizeX / 2, isPlayer1 ? 0 : Board.SizeY - 1);
            Gradiator = new Gradiator(initialPos, isPlayer1 ? GradiatorDirection.North : GradiatorDirection.South, this);
            Plots = new Plots(rule);
        }


        public IGradiator Gradiator { get; private set; }

        public ILife Life { get; private set; }

        public IHand Hand { get; private set; } = new Hand();

        public IPlots Plots { get; private set; }

        public IList<IAction> GetActualActions() {
            throw new NotImplementedException();
        }
    }

}
