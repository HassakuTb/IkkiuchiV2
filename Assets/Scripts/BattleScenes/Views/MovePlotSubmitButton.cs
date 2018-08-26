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
    [RequireComponent(typeof(CanvasGroup))]
    public class MovePlotSubmitButton : MonoBehaviour{

        public RpcInvoker rpcInvoker;

        private Controller controller;
        private PlotViewModel plotVM;

        private void Start() {
            controller = Controller.Instance;
            plotVM = PlotViewModel.Instance;

            GetComponent<LongClickFillButton>().onLongClick.AddListener(() => {
                rpcInvoker.InvokeRpcSubmitMovePlot(
                    controller.MyPlayer == controller.Player1,
                    plotVM.MovePlots.Select(c => c != null ? c.Id : -1).ToArray());
            });
        }

        private void Update() {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (plotVM.IsMovePlotFull()) {
                cg.blocksRaycasts = true;
                cg.alpha = 1f;
            }
            else {
                cg.blocksRaycasts = false;
                cg.alpha = 0.3f;
            }
        }
    }
}
