using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    class Menu
    {

        public static void Update()
        {
            if (Input.WasKeyPressed(Keys.P) == true)
            {
                if (GameRoot.pause == false)
                {
                    GameRoot.pause = true;
                }
                else
                {
                    GameRoot.pause = false;
                }
            }
            if (Input.WasKeyPressed(Keys.N) == true)
            {
                Round.Next();
            }



        }

    }
}
