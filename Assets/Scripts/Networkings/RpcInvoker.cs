using Ikkiuchi.Core;
using UnityEngine;
using Zenject;

namespace Ikkiuchi.Networkings {

    [RequireComponent(typeof(PhotonView))]
    public class RpcInvoker : MonoBehaviour{

        [Inject] private Controller controller;

        public void InvokeRpcSubmitMovePlot(bool isPlayer1, int[] plots) {
            GetComponent<PhotonView>().RPC("RPCSumbitMovePlot", PhotonTargets.All);
        }

        public void InvokeRpcSubmitActionPlot(bool isPlayer1, int[] plots) {
            GetComponent<PhotonView>().RPC("RPCSubmitActionPlot", PhotonTargets.All);
        }

        [PunRPC]
        private void RPCSumbitMovePlot(bool isPlayer1, int[] plots) {
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
