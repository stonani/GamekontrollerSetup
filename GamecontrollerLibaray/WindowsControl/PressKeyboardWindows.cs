using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace GamecontrollerLibaray
{
    public class PressKeyboardWindows : IPressKeyboard
    {
        InputSimulator input = new InputSimulator();

        public void Press(string key)
        {
            switch(key)
            {
                case "up":
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
                    break;
                case "down":
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
                    break;
                case "left":
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
                    break;
                case "right":
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
                    break;
            }

        }
    }
}
