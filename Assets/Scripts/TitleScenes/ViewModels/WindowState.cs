using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikkiuchi.TitleScenes.ViewModels {

    public enum WindowStateEnum {
        None,
        BuildRoom,
        WaitJoin,
        SelectRoom,
    }

    public class WindowState {

        public WindowStateEnum State { get; set; } = WindowStateEnum.None;
    }
}
