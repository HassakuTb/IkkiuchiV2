using UnityEngine;

namespace Ikkiuchi.Networkings {

    public interface IRpcInvoker {

    }


    [RequireComponent(typeof(PhotonView))]
    public class RpcInvoker : MonoBehaviour, IRpcInvoker{

        [PunRPC]
        private void RPCSumbitMovePlot(bool isPlayer1, int[] plots) {

        }

        [PunRPC]
        private void RPCSubmitActionPlot(bool isPlayer1, int[] plots) {

        }
    }
}
