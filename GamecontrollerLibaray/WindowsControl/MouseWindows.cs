using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using GamecontrollerLibaray.Interface;

namespace GamecontrollerLibaray.WindowsControl
{
    public class MouseWindows : IMouse
    {
        InputSimulator input = new InputSimulator();

        public void MoveMouse(int x, int y)
        {
            if (x>520)
            {
                input.Mouse.MoveMouseBy(10, 0);
            }
            if (x < 480)
            {
                input.Mouse.MoveMouseBy(-10, 0);
            }
            if (y > 520)
            {
                input.Mouse.MoveMouseBy(0, 10);
            }
            if (y < 480)
            {
                input.Mouse.MoveMouseBy(0, -10);
            }

        }

        public void LeftClickButton(bool button)
        {
            if (button)
            {
                input.Mouse.LeftButtonClick();
            }

        }
    }
}
