using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ikkiuchi.Networkings {
    public interface IPhotonNetworkWrapper {
        void ConnectUsingSettings();
    }
    public class PhotonNetworkWrapper : IPhotonNetworkWrapper {
        public void ConnectUsingSettings() {
            //  引数はゲームバージョン
            PhotonNetwork.ConnectUsingSettings(null);
        }
    }
}
