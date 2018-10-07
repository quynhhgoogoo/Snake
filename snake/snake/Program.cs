using System;


namespace snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x = new int[50];
               x[0]=10;
            int[] y = new int[50];
               y[0] = 10;
            int appleX = 20;
            int appleY = 20;
            int appleCounter = 0;

            string Action = "";

            int speed = 150;

            bool GameOn = true;
            bool WallHit = false;
            bool AppleEaten = false;
            bool StayInMenu = true;

            Random random = new Random();

            Console.CursorVisible = false;

            //Welcome screen


            //Give player option to read instruction
            ShowMenu(out Action);
            switch (Action)
            {
                case "1":
                case "d":
                case "directions":
                    Console.Clear();
                    BuidWall();
                    Console.SetCursorPosition(3, 3);
                    Console.WriteLine("Use arrows on keyboard to move your snake");
                    Console.SetCursorPosition(3, 4);
                    Console.WriteLine("Snake will die if you hit it to the wall");
                    Console.SetCursorPosition(3, 5);
                    Console.WriteLine("Your points equal to your apples eaten");
                    Console.SetCursorPosition(3, 6);
                    Console.WriteLine("Press Enter to return to the Menu");
                    Console.ReadLine();
                    Console.Clear();
                    ShowMenu(out Action);

                    break;

                case "2":
                case "p":
                case "play":
                    //Initialize snake on screen
                    Console.SetCursorPosition(x[0], y[0]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O");

                    //Set apple on screen
                    SetApplePosition(random, out appleX, out appleY);
                    drawApple(appleX, appleY);

                    //Build boundary
                    BuidWall();

                    //Snake movement
                    ConsoleKey command = Console.ReadKey().Key;
                    do
                    {
                        switch (command)
                        {
                            case ConsoleKey.LeftArrow:
                                Console.SetCursorPosition(x[0], y[0]);
                                Console.Write(" ");
                                x[0]--;
                                break;

                            case ConsoleKey.UpArrow:
                                Console.SetCursorPosition(x[0], y[0]);
                                Console.Write(" ");
                                y[0]--;
                                break;

                            case ConsoleKey.RightArrow:
                                Console.SetCursorPosition(x[0], y[0]);
                                Console.Write(" ");
                                x[0]++;
                                break;

                            case ConsoleKey.DownArrow:
                                Console.SetCursorPosition(x[0], y[0]);
                                Console.Write(" ");
                                y[0]++;
                                break;
                        }

                        //Paint snake
                        paintSnake(appleCounter, x, y, out x, out y);

                        //Detect when snake hits boundary
                        WallHit = SnakeHitWall(x[0], y[0]);
                        if (WallHit)
                        {
                            GameOn = false;
                            Console.SetCursorPosition(35, 20);
                            Console.Write("You die. Bye\n");

                            //Show score
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(10, 10);
                            Console.Write("Score : " + appleCounter);
                            Console.SetCursorPosition(10, 11);
                            Console.WriteLine("Press enter to continue");
                            appleCounter = 0;
                            Console.ReadLine();
                            Console.Clear();

                            //Give option to restart game
                            ShowMenu(out Action);
                        }

                        // Detect when apple was eaten
                        AppleEaten = determineIfAppleIsEaten(x[0], y[0], appleX, appleY);


                        //Place apple on board
                        if (AppleEaten)
                        {
                            SetApplePosition(random, out appleX, out appleY);
                            drawApple(appleX, appleY);
                            //Score calculation
                            appleCounter++;
                            //Make snake faster
                            speed++;
                        }


                        if (Console.KeyAvailable)
                        {
                            command = Console.ReadKey().Key;
                        }

                        //Slow game down
                        System.Threading.Thread.Sleep(speed);
                    } while (GameOn);

                    break;

                case "3":
                case "e":
                case "exit":
                    StayInMenu = false;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Input is invalid. Press Enter and try again");
                    Console.ReadLine();
                    Console.Clear();
                    ShowMenu(out Action);
                    break;
            }
        }

        private static void ShowMenu(out string Action)
        {
            string menu1 = "1)Instruction\n 2)Play\n 3)Exit\n\n";
            string menu2 = "1)Instruction\n 2)Play\n 3)Exit\n\n";
            string menu3 = "1)Instruction\n 2)Play\n 3)Exit\n\n";
            string menu4 = "1)Instruction\n 2)Play\n 3)Exit\n\n";
            string menu5 = "1)Instruction\n 2)Play\n 3)Exit\n\n";

            Console.WriteLine(menu1);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu2);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu3);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu4);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu5);
            System.Threading.Thread.Sleep(100);
            Console.Clear();
        }

        //Make snake longer
        private static void paintSnake(int appleCounter, int[] x1, int[] y1, out int[] x2, out int[] y2)
        {
            //Paint the head
            Console.SetCursorPosition(x1[0], y1[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("O");

            //Paint the rest of body
            for(int i =1; i< appleCounter + 1; i++)
            {
                Console.SetCursorPosition(x1[i], y1[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("o");
            }

            //Erase last part of snake
            Console.SetCursorPosition(x1[appleCounter+1], y1[appleCounter+1]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ");

            //Determine each body location
            for(int i = appleCounter+1; i>0; i--)
            {
                x1[i] = x1[i - 1];
                y1[i] = y1[i - 1];

            }

            //Return new array
            x2 = x1;
            y2 = y1;
        }

        private static bool SnakeHitWall(int x, int y)
        {
            if(x == 1 || x == 90 || y == 1 || y == 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void BuidWall()
        {
            //Build boundary for two vertical sides
            for(int i = 1; i<31; i++)
            {
                Console.SetCursorPosition(1,i);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("#");

                Console.SetCursorPosition(90, i);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("#");
            }

            //Build horizontal boundary for two horizontal sides
            for (int i = 1; i<91; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("#");

                Console.SetCursorPosition(i, 30);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("#");
            }
        }
        private static void SetApplePosition(Random random, out int appleX, out int appleY)
        {
            appleX = random.Next(2, 88);
            appleY = random.Next(2, 28);

        }

        private static void drawApple(int appleX, int appleY)
        {
            Console.SetCursorPosition(appleX, appleY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)214);
        }

        private static bool determineIfAppleIsEaten(int x, int y, int appleX, int appleY)
        {
            if(x==appleX && y == appleY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
