﻿using System.Collections.Generic;
using System.Linq;

namespace IkkiuchiV2.Core {
    public interface IPlots {

        //  移動をプロットする
        void PlotMove(int index, ICard card);

        //  行動をプロットする
        void PlotAction(int index, ICard card);


        bool IsMovePenarized(int index);

        bool IsActionPenarized(int index);

        bool IsMovePloted(int index);

        bool IsActionPloted(int index);

        ICard MovePloted(int index);

        ICard ActionPloted(int index);

        void OnTurnEnd(ITrash trash);
    }

    public class Plots : IPlots {

        private readonly ICard[] movePlot;
        private readonly ICard[] actionPlot;

        private readonly ICard[] movePenarty;
        private readonly ICard[] actionPenarty;

        public Plots(IRule rule) {
            movePlot = new ICard[rule.CountOfMoment];
            actionPlot = new ICard[rule.CountOfMoment];

            movePenarty = new ICard[rule.CountOfMoment];
            actionPenarty = new ICard[rule.CountOfMoment];
        }

        public void PlotAction(int index, ICard card) {
            actionPlot[index] = card;
        }

        public void PlotMove(int index, ICard card) {
            movePlot[index] = card;
        }

        public bool IsMovePenarized(int index) {
            return movePenarty[index] == null;
        }

        public bool IsActionPenarized(int index) {
            return actionPenarty[index] == null;
        }

        public bool IsMovePloted(int index) {
            return movePlot[index] != null;
        }

        public bool IsActionPloted(int index) {
            return actionPlot[index] != null;
        }

        public ICard MovePloted(int index) {
            return movePlot[index];
        }

        public ICard ActionPloted(int index) {
            return actionPlot[index];
        }

        public void OnTurnEnd(ITrash trash) {
            //  現時点のペナルティカードを全てゴミ箱へ
            movePenarty.Concat(actionPenarty)
                .Where(c => c != null)
                .Distinct()
                .Where(c => !c.Action.IsTrump)  //  切り札は捨てない
                .ForEach(c => {
                    trash.TrashCard(c);
                });

            //  ペナルティをクリア
            for(int i = 0; i < movePenarty.Length; ++i) {
                movePenarty[i] = null;
                actionPenarty[i] = null;
            }

            //  actionにプロットされているペナルティを反映
            for(int i = 0; i < actionPlot.Length; ++i) {
                if (actionPlot[i] == null) continue;
                if (actionPlot[i].Action.PenartyMove) {
                    movePenarty[i] = actionPlot[i];
                }
                if (actionPlot[i].Action.PenartyAction) {
                    actionPenarty[i] = actionPlot[i];
                }
            }

            //  プロットをクリア
            for (int i = 0; i < movePlot.Length; ++i) {
                movePlot[i] = null;
                actionPlot[i] = null;
            }
        }
    }
}
