using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamecontrollerLibaray.Interface
{
    public interface IMouse
    {
        void MoveMouse(int x, int y);
        void LeftClickButton(bool button);
    }
}
