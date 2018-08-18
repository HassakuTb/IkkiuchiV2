using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikkiuchi.Animations {
    public interface IAnimationEvent {
        void Start();
        void End();
    }
}
