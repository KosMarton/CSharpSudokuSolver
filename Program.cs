using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] rawSource = { "...7..3.1",
                                   "3..9.....",
                                   ".4.31.2..",
                                   ".6.4..5..",
                                   ".........",
                                   "..1..8.4.",
                                   "..6.21.5.",
                                   ".....9..8",
                                   "8.5..4..." };

            char[][] table = LoadTable(rawSource);
            int stepsCount = 0;
            if (Solve(table, ref stepsCount))
            {
                PrintTable(table, stepsCount);
            }
            else
            {
                Console.WriteLine("Could not solve this Sudoku.");
            }
        }

        static char[][] LoadTable(string[] rawSource)
        {
            char[][] table = new char[9][];

            for (int i = 0; i < 9; i++)
            {
                string line = rawSource[i];
                table[i] = line.PadRight(9).Substring(0, 9).ToCharArray();
                for (int j = 0; j < 9; j++)
                {
                    if (table[i][j] < '0' || table[i][j] > '9')
                    {
                        table[i][j] = '.';
                    }
                }
            }

            return table;
        }

        static void PrintTable(char[][] table, int stepsCount)
        {
            Console.WriteLine("Solved table after {0} steps:", stepsCount);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("{0}", new string(table[i]));
            }
        }

        static char[] GetCandidates(char[][] table, int row, int col)
        {
            string s = "";

            for (char c = '1'; c <= '9'; c++)
            {
                bool collision = false;

                for (int i = 0; i < 9; i++)
                {
                    if (table[row][i] == c || table[i][col] == c || table[(row - row % 3) + i / 3][(col - col % 3) + i % 3] == c)
                    {
                        collision = true;
                        break;
                    }
                }

                if (!collision)
                {
                    s += c;
                }
            }

            return s.ToCharArray();
        }

        static bool Solve(char[][] table, ref int stepsCount)
        {
            bool solved = false;

            int row = -1;
            int col = -1;
            char[] candidates = null;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (table[i][j] == '.')
                    {
                        char[] newCandidates = GetCandidates(table, i, j);
                        if (row < 0 || newCandidates.Length < candidates.Length)
                        {
                            row = i;
                            col = j;
                            candidates = newCandidates;
                        }
                    }
                }
            }

            if (row < 0)
            {
                solved = true;
            }
            else
            {
                for (int i = 0; i < candidates.Length; i++)
                {
                    table[row][col] = candidates[i];
                    stepsCount++;
                    if (Solve(table, ref stepsCount))
                    {
                        solved = true;
                        break;
                    }
                    table[row][col] = '.';
                }
            }

            return solved;
        }
    }
}