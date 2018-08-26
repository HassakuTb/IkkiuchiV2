using Ikkiuchi.Core;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.Networkings {

    [RequireComponent(typeof(PhotonView))]
    public class RpcInvoker : MonoBehaviour{

        private Controller controller;

        private void Start() {
            controller = Controller.Instance;
        }

        public void InvokeRpcSubmitMovePlot(bool isPlayer1, int[] plots) {
            if (PhotonNetwork.room != null) {
                GetComponent<PhotonView>().RPC("RPCSubmitMovePlot", PhotonTargets.All, isPlayer1, plots);
            }
            else {
                controller.MovePlotDebug(plots);
            }
        }

        public void InvokeRpcSubmitActionPlot(bool isPlayer1, int[] plots) {
            if (PhotonNetwork.room != null) {
                GetComponent<PhotonView>().RPC("RPCSubmitActionPlot", PhotonTargets.All, isPlayer1, plots);
            }
            else {
                controller.ActionPlotDebug(plots);
            }
        }

        [PunRPC]
        private void RPCSubmitMovePlot(bool isPlayer1, int[] plots) {
            controller.MovePlot(
                isPlayer1 ? controller.Player1 : controller.Player2,
                plots);
        }

        [PunRPC]
        private void RPCSubmitActionPlot(bool isPlayer1, int[] plots) {
            controller.ActionPlot(
                isPlayer1 ? controller.Player1 : controller.Player2,
                plots);
        }
    }
}
