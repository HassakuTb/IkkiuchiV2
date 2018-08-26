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

        private void Start() {
            GetComponent<LongClickFillButton>().onLongClick.AddListener(() => {
                rpcInvoker.InvokeRpcSubmitActionPlot(
                    Controller.Instance.MyPlayer == Controller.Instance.Player1,
                    PlotViewModel.Instance.ActionPlots.Select(c => c != null ? c.Id : -1).ToArray());
            });
        }
    }
}
