using System.Collections.Generic;

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
    }
}
