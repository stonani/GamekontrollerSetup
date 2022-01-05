using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.Interface;

namespace GamecontrollerLibaray.Controllers
{
    public class MouseController
    {
        private IMouse _mouse;

        public MouseController(IMouse mouse)
        {
            _mouse = mouse;
        }

        public void MoveMouse(int x, int y)
        {
            _mouse.MoveMouse(x, y);
        }

        public void LeftClickMouse(bool button)
        {
            _mouse.LeftClickButton(button);
        }
    }
}
