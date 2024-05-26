using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Com : Player
    {
        List<Card> knowCard = new List<Card>();    //알고있는 카드 정보
        int checkLoc;           //현재 찾고있는 장소
        public Queue<(int, int)> goPath = new Queue<(int, int)>();   //가고있는 루트

        public Com(List<Card> startCard)
        {
            addCard(startCard);
        }


        public bool CheckGoLocation()
        {
            bool check = false;
            if(knowCard != null)
            {
                for(int i = 0; i < knowCard.Count; i++)
                {
                    if(!knowCard[i].GetType().Equals(CardType.LOC))
                    {
                        break;
                    }
                    else
                    {
                        if (knowCard[i].getLocNum() == checkLoc)
                        {
                            //알고있는 정보중에 현재 가고있는 장소가 포함되면
                            return true;
                        }
                    }
                }
            }
            return check;
        }

       public  List<(int, int)> FindShortestPath(int[,] grid, (int, int) start, (int, int) goal)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };
            Queue<((int, int), List<(int, int)>)> queue = new Queue<((int, int), List<(int, int)>)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            queue.Enqueue((start, new List<(int, int)> { start }));
            visited.Add(start);

            while (queue.Count > 0)
            {
                var (current, path) = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;

                // 목표 지점에 도달하면 경로 반환
                if (current == goal)
                {
                    return path;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dRow[i];
                    int ny = y + dCol[i];

                    // Check if the new position is within the bounds and not visited
                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && !visited.Contains((nx, ny)))
                    {
                        // Skip walls
                        if (grid[nx, ny] == 1)
                            continue;

                        List<(int, int)> newPath = new List<(int, int)>(path) { (nx, ny) };

                        queue.Enqueue(((nx, ny), newPath));
                        visited.Add((nx, ny));
                    }
                }
            }

            return null; // 경로를 찾지 못한 경우
        }




    }
}
