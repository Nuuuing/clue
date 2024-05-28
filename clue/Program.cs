using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace clue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Intro intro = new Intro();
            intro.RunIntro();
            if ( intro.EndIntro ==true)
            {
                Game game = new Game();
                game.Run();
            }
        }
    }
}

