using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectMaze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[,] board = new char[23, 53];

            Random random = new Random();
            bool smash = false;
            int healtPoint = 5;
            int point = 0;
            int number = 0;
            int count = 0, timer = 0;
            bool placed = false;

            int counterZero = 0;
            int numberSeriesLength = 1;
            int playerdirx = 0;
            int playerdiry = 0;
            int playerx = random.Next(1, 52);
            int playery = random.Next(1, 22);
            int[] ZeroXLocation = new int[40];
            int[] ZeroYLocation = new int[40];


            void GenerateNumber()
            {
                int numbery = 0, numberx = 0;
                while (board[numbery, numberx] != ' ')
                {
                    numbery = random.Next(1, 22);
                    numberx = random.Next(1, 52);

                    if (board[numbery, numberx] == ' ')
                    {
                        board[numbery, numberx] = numbers[random.Next(5, 10)];
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(numberx, numbery);
                Console.WriteLine(board[numbery, numberx]);
                Console.SetCursorPosition(63, 13);
                Console.WriteLine($"   a new {board[numbery, numberx]} added to the board ({numberx},{numbery})   ");
                Console.ResetColor();
            }

            void Countdown()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.White;
                for (int i = 1; i < 22; i++)
                {
                    for (int j = 1; j < 52; j++)
                    {
                        if (board[i, j] == '9')
                        {
                            board[i, j] = '8';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('8');
                        }
                        else if (board[i, j] == '8')
                        {
                            board[i, j] = '7';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('7');
                        }
                        else if (board[i, j] == '7')
                        {
                            board[i, j] = '6';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('6');
                        }
                        else if (board[i, j] == '6')
                        {
                            board[i, j] = '5';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('5');
                        }
                        else if (board[i, j] == '5')
                        {
                            board[i, j] = '4';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('4');
                        }
                        else if (board[i, j] == '4')
                        {
                            board[i, j] = '3';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('3');
                        }
                        else if (board[i, j] == '3')
                        {
                            board[i, j] = '2';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('2');
                        }
                        else if (board[i, j] == '2')
                        {
                            board[i, j] = '1';
                            Console.SetCursorPosition(j, i);
                            Console.WriteLine('1');
                        }
                        else if (board[i, j] == '1')
                        {
                            int tempNumber = random.Next(0, 100);
                            if (tempNumber < 3)
                            {
                                counterZero++;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.BackgroundColor = ConsoleColor.Black;
                                board[i, j] = '0';
                                Console.SetCursorPosition(j, i);
                                Console.WriteLine('0');
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.BackgroundColor = ConsoleColor.White;
                            }
                        }

                    }
                }
                Console.ResetColor();
            }

            void CreateBoard()
            {
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 53; j++)
                    {
                        if (i == 0 || i == 22)
                            board[i, j] = '#';
                        else if (j == 0 || j == 52)
                            board[i, j] = '#';
                        else
                            board[i, j] = ' ';
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    int hor = random.Next(0, 2);
                    do
                    {
                        PlaceWall(random.Next(1, 52), random.Next(1, 22), 11, hor == 0 ? true : false);
                    } while (placed == false);

                }
                for (int i = 0; i < 5; i++)
                {
                    int hor = random.Next(0, 2);
                    do
                    {
                        PlaceWall(random.Next(1, 52), random.Next(1, 22), 7, hor == 0 ? true : false);
                    } while (placed == false);

                }
                for (int i = 0; i < 20; i++)
                {
                    int hor = random.Next(0, 2);
                    do
                    {
                        PlaceWall(random.Next(1, 52), random.Next(1, 22), 3, hor == 0 ? true : false);
                    } while (placed == false);
                }
                int numbery = 0, numberx = 0;
                for (int i = 0; i < 70; i++)
                {
                    while (board[numbery, numberx] != ' ')
                    {
                        numbery = random.Next(1, 22);
                        numberx = random.Next(1, 52);

                        if (board[numbery, numberx] == ' ')
                        {
                            board[numbery, numberx] = numbers[random.Next(0, 10)];
                            break;
                        }
                    }
                }
            }

            void ShowBoard()
            {
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 53; j++)
                    {
                        if (board[i, j] == '#')
                        {
                            Console.BackgroundColor = ConsoleColor.White;  //Color adjustemnt for wall
                            Console.ForegroundColor = ConsoleColor.Black;  //Color adjustemnt for wall
                            Console.Write(board[i, j]);
                            Console.ResetColor();
                        }
                        else if (board[i, j] == '0')
                        {
                            Console.BackgroundColor = ConsoleColor.Black;   //Color adjustemnt for number zero
                            Console.ForegroundColor = ConsoleColor.Yellow;  //Color adjustemnt for number zero
                            Console.Write(board[i, j]);
                            Console.ResetColor();
                        }
                        else if (board[i, j] == ' ')
                        {
                            Console.BackgroundColor = ConsoleColor.White;   //Color adjustemnt for empty square
                            Console.Write(board[i, j]);
                            Console.ResetColor();
                        }
                        else if (board[i, j] == 'P')
                        {
                            Console.BackgroundColor = ConsoleColor.White;   //Color adjustemnt for player
                            Console.ForegroundColor = ConsoleColor.Red;     //Color adjustemnt for player
                            Console.Write(board[i, j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;   //Color adjustemnt for numbers
                            Console.ForegroundColor = ConsoleColor.Green;   //Color adjustemnt for numbers
                            Console.Write(board[i, j]);
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
            }

            bool PlaceWall(int oldX, int oldY, int length, bool isHorizontal)
            {
                for (int i = 0; i < length; i++)
                {
                    int x = isHorizontal ? oldX + i : oldX;
                    int y = isHorizontal ? oldY : oldY + i;
                    if (!IsPlaceable(x, y))
                    {
                        placed = false;
                        return false;
                    }
                }

                for (int i = 0; i < length; i++)
                {
                    int x = isHorizontal ? oldX + i : oldX;
                    int y = isHorizontal ? oldY : oldY + i;
                    board[y, x] = '#';

                }
                placed = true;
                return true;

            }

            bool IsPlaceable(int x, int y)
            {
                int[] toBeCheck = { -1, 0, 1, 0, -1, -1, 1, 1 };
                int[] toBeCheck2 = { 0, -1, 0, 1, -1, 1, -1, 1 };

                for (int i = 0; i < toBeCheck.Length; i++)
                {
                    int checkX = x + toBeCheck[i];
                    int checkY = y + toBeCheck2[i];


                    if (checkX <= 0 || checkX >= 53 || checkY <= 0 || checkY >= 23)
                        return false;

                    if (board[checkY, checkX] != ' ')
                        return false;
                }

                return true;
            }

            bool IsPushable(int x, int y)
            {
                int nextSquareX;
                int nextSquareY;
                if (playerdirx == 1)
                {
                    numberSeriesLength = 1;
                    nextSquareX = x + 2;
                    nextSquareY = y;
                    while (board[nextSquareY, nextSquareX] != ' ' && board[nextSquareY, nextSquareX] != '#')
                    {
                        if (board[nextSquareY, nextSquareX] > board[y, x + 1])
                            return false;
                        numberSeriesLength += 1;
                        nextSquareX++;
                        x++;
                    }
                    if (board[nextSquareY, nextSquareX] == '#' && numberSeriesLength == 1)
                        return false;
                    return true;
                }
                else if (playerdirx == -1)
                {
                    numberSeriesLength = 1;
                    nextSquareX = x - 2;
                    nextSquareY = y;
                    while (board[nextSquareY, nextSquareX] != ' ' && board[nextSquareY, nextSquareX] != '#')
                    {
                        if (board[nextSquareY, nextSquareX] > board[y, x - 1])
                            return false;
                        numberSeriesLength += 1;
                        nextSquareX--;
                        x--;
                    }
                    if (board[nextSquareY, nextSquareX] == '#' && numberSeriesLength == 1)
                        return false;
                    return true;
                }
                else if (playerdiry == 1)
                {
                    numberSeriesLength = 1;
                    nextSquareX = x;
                    nextSquareY = y + 2;
                    while (board[nextSquareY, nextSquareX] != ' ' && board[nextSquareY, nextSquareX] != '#')
                    {
                        if (board[nextSquareY, nextSquareX] > board[y + 1, x])
                            return false;
                        numberSeriesLength += 1;
                        nextSquareY++;
                        y++;
                    }
                    if (board[nextSquareY, nextSquareX] == '#' && numberSeriesLength == 1)
                        return false;
                    return true;
                }
                else if (playerdiry == -1)
                {
                    numberSeriesLength = 1;
                    nextSquareX = x;
                    nextSquareY = y - 2;
                    while (board[nextSquareY, nextSquareX] != ' ' && board[nextSquareY, nextSquareX] != '#')
                    {
                        if (board[nextSquareY, nextSquareX] > board[y - 1, x])
                            return false;
                        numberSeriesLength += 1;
                        nextSquareY--;
                        y--;
                    }
                    if (board[nextSquareY, nextSquareX] == '#' && numberSeriesLength == 1)
                        return false;
                    return true;
                }

                return true;
            }

            bool zeroMovement(int x, int y)
            {
                int zeroX;
                int zeroY;

                if (board[y, x + 1] != ' ' && board[y, x - 1] != ' ' && board[y + 1, x] != ' ' && board[y - 1, x] != ' ')
                    return false;

                do
                {
                    int randomdir = random.Next(0, 2);

                    if (randomdir == 0)
                    {
                        randomdir = random.Next(0, 2);
                        if (randomdir == 0)
                        {
                            zeroX = x + 1;
                            zeroY = y;
                        }
                        else
                        {
                            zeroX = x - 1;
                            zeroY = y;
                        }
                    }
                    else
                    {
                        randomdir = random.Next(0, 2);
                        if (randomdir == 0)
                        {
                            zeroX = x;
                            zeroY = y + 1;
                        }
                        else
                        {
                            zeroX = x;
                            zeroY = y - 1;
                        }
                    }
                    if (board[zeroY, zeroX] == 'P')
                    {
                        healtPoint--;
                        zeroX = x;
                        zeroY = y;
                        goto okusınav;
                    }

                } while (board[zeroY, zeroX] != ' ');

            okusınav:


                //if (board[zeroY, zeroX] != ' ') return false;

                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = ConsoleColor.White;
                board[y, x] = ' ';
                Console.WriteLine(' ');
                Console.SetCursorPosition(zeroX, zeroY);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                board[zeroY, zeroX] = '0';
                Console.WriteLine('0');
                Console.ResetColor();

                return true;
            }
            CreateBoard();
            ShowBoard();

            bool IsDead = false;
            // --- Main game loop
            while (IsDead == false)
            {


                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    if ((cki.Key == ConsoleKey.D || cki.Key == ConsoleKey.RightArrow) && playerx < 51 && board[playery, playerx + 1] == ' ' || board[playery, playerx + 1] == '0')
                    {
                        if (board[playery, playerx + 1] == '0')
                        {
                            healtPoint--;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White; //Color adjustemnt for empty square
                            board[playery, playerx] = ' ';
                            Console.WriteLine(' ');
                            playerx++;
                        }


                    }

                    else if ((cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.LeftArrow) && playerx > 1 && board[playery, playerx - 1] == ' ' || board[playery, playerx - 1] == '0')
                    {
                        if (board[playery, playerx - 1] == '0')
                        {
                            healtPoint--;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White; //Color adjustemnt for empty square
                            board[playery, playerx] = ' ';
                            Console.WriteLine(' ');
                            playerx--;
                        }



                    }
                    else if ((cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.UpArrow) && playery > 1 && board[playery - 1, playerx] == ' ' || board[playery - 1, playerx] == '0')
                    {
                        if (board[playery - 1, playerx] == '0')
                        {
                            healtPoint--;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White; //Color adjustemnt for empty square
                            board[playery, playerx] = ' ';
                            Console.WriteLine(' ');

                            playery--;
                        }

                    }
                    else if ((cki.Key == ConsoleKey.S || cki.Key == ConsoleKey.DownArrow) && playery < 21 && board[playery + 1, playerx] == ' ' || board[playery + 1, playerx] == '0')
                    {
                        if (board[playery + 1, playerx] == '0')
                        {
                            healtPoint--;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White; //Color adjustemnt for empty square
                            board[playery, playerx] = ' ';
                            Console.WriteLine(' ');
                            playery++;
                        }

                    }
                    else if ((cki.Key == ConsoleKey.D || cki.Key == ConsoleKey.RightArrow) && playerx < 51 && (board[playery, playerx + 1] != ' ' && board[playery, playerx + 1] != '#' && board[playery, playerx + 1] != '0'))
                    {
                        playerdirx = 1;
                        playerdiry = 0;
                        if (IsPushable(playerx, playery))
                        {
                            if (board[playery, playerx + numberSeriesLength + 1] == '#')
                                smash = true;
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                board[playery, playerx + i + 1] = board[playery, playerx + i];
                            }
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                if (board[playery, playerx + i + 1] == '0')
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.BackgroundColor = ConsoleColor.White;
                                }
                                Console.SetCursorPosition(playerx + i + 1, playery);
                                Console.WriteLine(board[playery, playerx + i + 1]);
                                Console.ResetColor();
                            }
                            if (smash)
                            {
                                if (board[playery, playerx + numberSeriesLength + 1] == '5' || board[playery, playerx + numberSeriesLength + 1] == '6' || board[playery, playerx + numberSeriesLength + 1] == '7' || board[playery, playerx + numberSeriesLength + 1] == '8' || board[playery, playerx + numberSeriesLength + 1] == '9')
                                    number = 1;
                                else if (board[playery, playerx + numberSeriesLength + 1] == '1' || board[playery, playerx + numberSeriesLength + 1] == '2' || board[playery, playerx + numberSeriesLength + 1] == '3' || board[playery, playerx + numberSeriesLength + 1] == '4')
                                    number = 2;
                                else if (board[playery, playerx + numberSeriesLength + 1] == '0')
                                {
                                    counterZero--;
                                    number = 20;
                                }
                                point += number;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                board[playery, playerx + numberSeriesLength + 1] = '#';
                                Console.SetCursorPosition(playerx + numberSeriesLength + 1, playery);
                                Console.WriteLine('#');
                                GenerateNumber();
                                smash = false;
                            }

                            board[playery, playerx + 1] = ' ';
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.WriteLine(' ');
                            Console.ResetColor();
                            board[playery, playerx] = ' ';
                            playerx++;
                        }
                    }
                    else if ((cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.LeftArrow) && playerx > 1 && (board[playery, playerx - 1] != ' ' && board[playery, playerx - 1] != '#' && board[playery, playerx - 1] != '0'))
                    {
                        playerdirx = -1;
                        playerdiry = 0;
                        if (IsPushable(playerx, playery))
                        {
                            if (board[playery, playerx - numberSeriesLength - 1] == '#')
                                smash = true;
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                board[playery, playerx - i - 1] = board[playery, playerx - i];
                            }
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                if (board[playery, playerx - i - 1] == '0')
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.BackgroundColor = ConsoleColor.White;
                                }

                                Console.SetCursorPosition(playerx - i - 1, playery);
                                Console.WriteLine(board[playery, playerx - i - 1]);
                                Console.ResetColor();

                            }
                            if (smash)
                            {
                                if (board[playery, playerx - numberSeriesLength - 1] == '5' || board[playery, playerx - numberSeriesLength - 1] == '6' || board[playery, playerx - numberSeriesLength - 1] == '7' || board[playery, playerx - numberSeriesLength - 1] == '8' || board[playery, playerx - numberSeriesLength - 1] == '9')
                                    number = 1;
                                else if (board[playery, playerx - numberSeriesLength - 1] == '1' || board[playery, playerx - numberSeriesLength - 1] == '2' || board[playery, playerx - numberSeriesLength - 1] == '3' || board[playery, playerx - numberSeriesLength - 1] == '4')
                                    number = 2;
                                else if (board[playery, playerx - numberSeriesLength - 1] == '0')
                                {
                                    counterZero--;
                                    number = 20;
                                }

                                point += number;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                board[playery, playerx - numberSeriesLength - 1] = '#';
                                Console.SetCursorPosition(playerx - numberSeriesLength - 1, playery);
                                Console.WriteLine('#');
                                GenerateNumber();
                                smash = false;
                            }
                            board[playery, playerx - 1] = ' ';
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.WriteLine(' ');
                            Console.ResetColor();
                            board[playery, playerx] = ' ';
                            playerx--;
                        }
                    }
                    else if ((cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.UpArrow) && playery > 1 && (board[playery - 1, playerx] != ' ' && board[playery - 1, playerx] != '#' && board[playery - 1, playerx] != '0'))
                    {
                        playerdirx = 0;
                        playerdiry = -1;
                        if (IsPushable(playerx, playery))
                        {
                            if (board[playery - numberSeriesLength - 1, playerx] == '#')
                                smash = true;
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                board[playery - i - 1, playerx] = board[playery - i, playerx];
                            }
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                if (board[playery - i - 1, playerx] == '0')
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.BackgroundColor = ConsoleColor.White;
                                }
                                Console.SetCursorPosition(playerx, playery - i - 1);
                                Console.WriteLine(board[playery - i - 1, playerx]);
                                Console.ResetColor();

                            }
                            if (smash)
                            {
                                if (board[playery - numberSeriesLength - 1, playerx] == '5' || board[playery - numberSeriesLength - 1, playerx] == '6' || board[playery - numberSeriesLength - 1, playerx] == '7' || board[playery - numberSeriesLength - 1, playerx] == '8' || board[playery - numberSeriesLength - 1, playerx] == '9')
                                    number = 1;
                                else if (board[playery - numberSeriesLength - 1, playerx] == '1' || board[playery - numberSeriesLength - 1, playerx] == '2' || board[playery - numberSeriesLength - 1, playerx] == '3' || board[playery - numberSeriesLength - 1, playerx] == '4')
                                    number = 2;
                                else if (board[playery - numberSeriesLength - 1, playerx] == '0')
                                {
                                    counterZero--;
                                    number = 20;
                                }
                                point += number;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                board[playery - numberSeriesLength - 1, playerx] = '#';
                                Console.SetCursorPosition(playerx, playery - numberSeriesLength - 1);
                                Console.WriteLine('#');
                                GenerateNumber();
                                smash = false;
                            }
                            board[playery - 1, playerx] = ' ';
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.WriteLine(' ');
                            Console.ResetColor();
                            board[playery, playerx] = ' ';
                            playery--;
                        }
                    }
                    else if ((cki.Key == ConsoleKey.S || cki.Key == ConsoleKey.DownArrow) && playery < 21 && (board[playery + 1, playerx] != ' ' && board[playery + 1, playerx] != '#' && board[playery + 1, playerx] != '0'))
                    {
                        playerdirx = 0;
                        playerdiry = 1;
                        if (IsPushable(playerx, playery))
                        {
                            if (board[playery + numberSeriesLength + 1, playerx] == '#')
                                smash = true;
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                board[playery + i + 1, playerx] = board[playery + i, playerx];
                            }
                            for (int i = numberSeriesLength; i > 0; i--)
                            {
                                if (board[playery + i + 1, playerx] == '0')
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.BackgroundColor = ConsoleColor.White;
                                }
                                Console.SetCursorPosition(playerx, playery + i + 1);
                                Console.WriteLine(board[playery + i + 1, playerx]);
                                Console.ResetColor();

                            }

                            if (smash)
                            {
                                if (board[playery + numberSeriesLength + 1, playerx] == '5' || board[playery + numberSeriesLength + 1, playerx] == '6' || board[playery + numberSeriesLength + 1, playerx] == '7' || board[playery + numberSeriesLength + 1, playerx] == '8' || board[playery + numberSeriesLength + 1, playerx] == '9')
                                    number = 1;
                                else if (board[playery + numberSeriesLength + 1, playerx] == '1' || board[playery + numberSeriesLength + 1, playerx] == '2' || board[playery + numberSeriesLength + 1, playerx] == '3' || board[playery + numberSeriesLength + 1, playerx] == '4')
                                    number = 2;
                                else if (board[playery + numberSeriesLength + 1, playerx] == '0')
                                {
                                    counterZero--;
                                    number = 20;
                                }
                                point += number;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                board[playery + numberSeriesLength + 1, playerx] = '#';
                                Console.SetCursorPosition(playerx, playery + numberSeriesLength + 1);
                                Console.WriteLine('#');
                                GenerateNumber();
                                smash = false;
                            }
                            board[playery + 1, playerx] = ' ';
                            Console.SetCursorPosition(playerx, playery);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.WriteLine(' ');
                            Console.ResetColor();
                            board[playery, playerx] = ' ';
                            playery++;
                        }
                    }


                    if (healtPoint == 0)
                    {
                        IsDead = true;
                        Console.Clear();
                       
                    }



                    if (cki.Key == ConsoleKey.Escape) break;
                }

                Console.SetCursorPosition(playerx, playery);
                Console.BackgroundColor = ConsoleColor.White; //Color adjustment for player
                Console.ForegroundColor = ConsoleColor.Red;   //Color adjustment for player
                board[playery, playerx] = 'P';
                Console.WriteLine('P');
                Console.ResetColor();




                count++;

                if (count % 20 == 0)
                {
                    counterZero = 0;
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {
                            if (board[i, j] == '0')
                            {
                                ZeroXLocation[counterZero] = j;
                                ZeroYLocation[counterZero] = i;
                                counterZero++;
                            }
                        }
                    }
                    timer++;
                    for (int i = 0; i < counterZero; i++)
                        zeroMovement(ZeroXLocation[i], ZeroYLocation[i]);


                }
                if (count % 300 == 0)
                    Countdown();
                Console.SetCursorPosition(65, 8);

                Console.WriteLine("Zero Count: " + counterZero + "  ");
                Console.SetCursorPosition(65, 10);
                Console.WriteLine("Timer: " + timer);
                Console.SetCursorPosition(65, 11);
                Console.WriteLine("Points: " + point);
                Console.SetCursorPosition(65, 12);
                Console.WriteLine("Health: " + healtPoint);
                Thread.Sleep(50);
            }

        }
    }
}

