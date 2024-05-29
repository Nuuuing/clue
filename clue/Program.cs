using System;
using System.Text;

namespace clue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            /*
            Intro intro = new Intro();
            intro.RunIntro();

            if ( intro.EndIntro ==true)
            {
            */
            Game game = new Game();
                game.Run();
           // }
        }
    }
}