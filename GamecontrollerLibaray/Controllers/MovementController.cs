using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamecontrollerLibaray.Controllers
{
    public class MovementController
    {
        private IPressKeyboard _keyboard;

        public MovementController(IPressKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public void HandleMovement(List<string> directions)
        {
            foreach (var direction in directions)
            {
                _keyboard.Press(direction);
            }
        }
    }
}
