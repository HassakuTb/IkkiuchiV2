using System.Collections.Generic;

namespace IkkiuchiV2.Core {
    public interface IPlots {

        //  移動をプロットする
        void PlotMove(int index, ICard card);

        //  行動をプロットする
        void PlotAction(int index, ICard card);
    }

    public class Plots : IPlots {

        private ICard[] movePlot;
        private ICard[] actionPlot;

        private ICard[] movePenarty;
        private ICard[] actionPenarty;

        public Plots(IRule rule) {
            movePlot = new ICard[rule.CountOfMoment];
            actionPlot = new ICard[rule.CountOfMoment];
        }

        public void PlotAction(int index, ICard card) {
            actionPlot[index] = card;
        }

        public void PlotMove(int index, ICard card) {
            movePlot[index] = card;
        }
    }
}
