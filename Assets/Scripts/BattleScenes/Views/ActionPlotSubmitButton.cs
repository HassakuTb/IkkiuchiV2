using Ikkiuchi.BattleScenes.ViewModels;
using Ikkiuchi.Core;
using Ikkiuchi.Networkings;
using System.Linq;
using UI;
using UnityEngine;
using UniRx;
using Zenject;

namespace Ikkiuchi.BattleScenes.Views {
    [RequireComponent(typeof(LongClickFillButton))]
    public class ActionPlotSubmitButton : MonoBehaviour{

        public RpcInvoker rpcInvoker;

        [Inject] private Controller controller;
        [Inject] private PlotViewModel plotVM;

        private void Start() {
            GetComponent<LongClickFillButton>().onLongClick.AddListener(() => {
                rpcInvoker.InvokeRpcSubmitActionPlot(
                    controller.MyPlayer == controller.Player1,
                    plotVM.ActionPlots.Select(c => c != null ? c.Id : -1).ToArray());
            });
        }
    }
}
