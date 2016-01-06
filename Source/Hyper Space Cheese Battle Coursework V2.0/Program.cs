using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyper_Space_Cheese_Battle_Coursework_V2._0
{
    #region Direction Enum
    enum Direction //An enum that is storing all the possible directions for the game board
    {
        Right,
        Left,
        Up,
        Down,
        Win,
        Start,
        RightCheese,
        LeftCheese,
        UpCheese
    }
    #endregion

    #region Player Struct
    public struct Player // A struct which stores all the values for the players
    {
        public int PlayerNo;
        public string Name;
        public int X;
        public int Y;
        public ConsoleColor PlayerColour;
    }
    #endregion

    class Program
    {
        #region Player Array
        public static Player[] players; // The array that stores each player
        #endregion

        #region Number Of Players Variable
        public static int NoPlayers; // A number of players variable that is required throughout the program 
        #endregion

        #region Won Variable
        public static bool won = false;
        #endregion

        #region Testing Mode
        public static bool TestingMode; // A testing mode boolean that is required in several parts of the program
        #endregion

        #region Dice Roll
        public static int DiceThrow() // The 'DiceThrow' method. Used to simulate rolling a dice. This also contains a testing mode which, if true, allows the user to input a dice roll value
        {
            if (TestingMode == true)
            {
                bool AskAgain;
                do
                {
                    AskAgain = false;
                    try
                    {
                        BoardDraw();
                        Console.Write("\nPlease enter a dice value: ");
                        int TestingDice = int.Parse(Console.ReadLine());
                        System.Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("HyperSpace Cheese Battle\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        return TestingDice;
                    }
                    catch
                    {
                        Console.WriteLine("That input is invalid");
                        AskAgain = true;
                    }
                } while (AskAgain == true);
            }
            Random random = new Random();
            int randomNumber = random.Next(1, 7);
            Console.WriteLine("You rolled a {0}", randomNumber);
            return randomNumber;
        }
        #endregion

        #region Game Board
        public static Direction[,] board = new Direction[,] //The game board, created from the 'Direction' enum
        { 
            //Collumn 0                 //Column 1          //Column 2          //Column 3                  //Column 4              //Column 5              //Column 6                  //Column 7
                
            {Direction.Up,              Direction.Up,       Direction.Up,       Direction.Up,               Direction.Up,           Direction.Up,           Direction.Up,               Direction.Up},      // Row 0 
            {Direction.Right,           Direction.Right,    Direction.Up,       Direction.Down,             Direction.Up,           Direction.Up,           Direction.Left,             Direction.Left},    // Row 1 
            {Direction.Right,           Direction.Right,    Direction.Up,       Direction.Right,            Direction.UpCheese,     Direction.Left,         Direction.Left,             Direction.Left},    // Row 2 
            {Direction.RightCheese,     Direction.Right,    Direction.Up,       Direction.Right,            Direction.Left,         Direction.Up,           Direction.Left,             Direction.Left},    // Row 3 
            {Direction.Right,           Direction.Right,    Direction.Up,       Direction.Right,            Direction.Up,           Direction.Up,           Direction.LeftCheese,       Direction.Left},    // Row 4 
            {Direction.Right,           Direction.Right,    Direction.Right,    Direction.RightCheese,      Direction.Up,           Direction.Up,           Direction.Left,             Direction.Left},    // Row 5 
            {Direction.Right,           Direction.Right,    Direction.Up,       Direction.Down,             Direction.Up,           Direction.Left,         Direction.Left,             Direction.Left},    // Row 6 
            {Direction.Down,            Direction.Right,    Direction.Right,    Direction.Right,            Direction.Right,        Direction.Right,        Direction.Down,             Direction.Win}      // Row 7 
        };
        #endregion

        #region Player Moving
        public static void Move(int x, int y, int player) // The 'Move' method. This contains a player collisions checker and a border collisions checker
        {
            foreach (Player p in players) // The player collisions checker
            {
                if (players[player].PlayerNo == p.PlayerNo) // This stops the player from colliding with themself
                {
                    continue;
                }
                if (players[player].Y + y == p.Y && players[player].X + x == p.X) // This checks if the player has collided with another player
                {
                    players[player].X += x;
                    players[player].Y += y;
                    switch (board[p.Y, p.X]) // This checks to see what direction the player, that the current player has collided with, is going and to then move the current player 1 square in the required direction
                    {
                        case Direction.Down:
                            Move(0, -1, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves down", players[player].Name, p.Name);
                            break;
                        case Direction.Up:
                            Move(0, +1, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves up", players[player].Name, p.Name);
                            break;
                        case Direction.Right:
                            Move(+1, 0, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves right", players[player].Name, p.Name);
                            break;
                        case Direction.Left:
                            Move(-1, 0, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves left", players[player].Name, p.Name);
                            break;
                        case Direction.RightCheese:
                            Move(+1, 0, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves right", players[player].Name, p.Name);
                            break;
                        case Direction.LeftCheese:
                            Move(-1, 0, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves left", players[player].Name, p.Name);
                            break;
                        case Direction.UpCheese:
                            Move(0, +1, player);
                            Console.WriteLine("Looks like that square is already taken");
                            Console.WriteLine("{0} bounces off {1} and moves up", players[player].Name, p.Name);
                            break;
                    }
                    return;
                }
            }
            Console.WriteLine("{0}'s previous coordinates were ({1},{2})", players[player].Name, players[player].X, players[player].Y);
            if (players[player].X + x > 7 || players[player].Y + y > 7 || players[player].X + x < 0 || players[player].Y + y < 0) // This checks to see if the player has gone over the board and takes them back to their original position if so
            {
                Console.WriteLine("Looks like {0} has gone over the board", players[player].Name);
                Console.WriteLine("{0} is returned to their previous position", players[player].Name);
                return;
            }
            players[player].X += x; // These use values from the Player Turn method
            players[player].Y += y; // and adds them to the X and Y values of the player
        }
        #endregion

        #region Player Turn
        public static void PlayerTurn(int player) // The player turn method which uses the player movement method (Move) to move each player. It also has a Cheese checker.
        {
            int TempDice = DiceThrow();
            switch (board[players[player].Y, players[player].X]) // This checks to see what direction the player is currently on and then moves them accordingly using 'DiceThrow' and 'Move' method
            {
                case Direction.Down:
                    Move(0, -TempDice, player);
                    break;
                case Direction.Up:
                    Move(0, TempDice, player);
                    break;
                case Direction.Right:
                    Move(TempDice, 0, player);
                    break;
                case Direction.Left:
                    Move(-TempDice, 0, player);
                    break;
                case Direction.RightCheese:
                    Move(TempDice, 0, player);
                    break;
                case Direction.LeftCheese:
                    Move(-TempDice, 0, player);
                    break;
                case Direction.UpCheese:
                    Move(0, TempDice, player);
                    break;
            }
            CheesePowerUp(player); // This calls the 'CheesePowerUp' method for the player to check see if they have landed on a cheese powerup square and act accordingly
            if (TempDice == 6)
            {
                BoardDraw();
                Console.WriteLine("\n{0} rolled a 6 so they can roll again", players[player].Name);
                Console.Write("Press enter to roll again");
                Console.ReadLine();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("HyperSpace Cheese Battle\n");
                Console.ForegroundColor = ConsoleColor.White;
                PlayerTurn(player);
            }
        }
        #endregion

        #region Drawing The Board
        static void BoardDraw() // The 'BoardDraw' method which draws the board miraculously enough
        {
            Console.WriteLine();
            for (int x = 7; x > -1; x--)
            {
                for (int y = 0; y < 8; y++)
                {
                    foreach (Player p in players) // This colours the position that the player is on to their specific player colour on the game board
                    {
                        if (p.X == y && p.Y == x)
                        {
                            Console.ForegroundColor = p.PlayerColour;
                        }
                    }
                    switch (board[x, y])
                    {
                        case Direction.Up:
                            Console.Write("  ↑  ");
                            break;
                        case Direction.Down:
                            Console.Write("  ↓  ");
                            break;
                        case Direction.Right:
                            Console.Write("  →  ");
                            break;
                        case Direction.Left:
                            Console.Write("  ←  ");
                            break;
                        case Direction.UpCheese:
                            Console.Write(" |↑| ");
                            break;
                        case Direction.RightCheese:
                            Console.Write(" |→| ");
                            break;
                        case Direction.LeftCheese:
                            Console.Write(" |←| ");
                            break;
                        case Direction.Win:
                            Console.Write("  WIN  ");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Cheese Powerup
        public static void CheesePowerUp(int player) // The 'CheesePowerUp' method. This first checks to see if the player has landed on a cheese powerup square and if so presents the player with a choice to do another roll or to shoot another player
        {
            if (players[player].X == 0 && players[player].Y == 3 || players[player].X == 3 && players[player].Y == 5
                || players[player].X == 4 && players[player].Y == 2 || players[player].X == 6 && players[player].Y == 4) // The cheese powerup position checker
            {
                bool AskAgain1;
                do
                {
                    BoardDraw();
                    Console.WriteLine("\n{0} has landed on a cheese powerup", players[player].Name);
                    Console.Write("Would you like to roll again or shoot another player? r (roll) or s (shoot): ");
                    string Choice = Console.ReadLine();
                    Choice.ToUpper();
                    System.Console.Clear();
                    AskAgain1 = false;
                    switch (Choice) // The choice for the player to either do another roll or to shoot another player
                    {
                        case "r":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("HyperSpace Cheese Battle\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            BoardDraw();
                            Console.WriteLine("\n{0} chose to roll again", players[player].Name);
                            Console.WriteLine("Press enter to roll the dice");
                            Console.ReadLine();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("HyperSpace Cheese Battle\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            PlayerTurn(player);
                            break;
                        case "s":
                            bool AskAgain;
                            int ShootChoice;
                            bool AskAgain3;
                            do
                            {
                                AskAgain3 = false;
                                try
                                {
                                    do
                                    {
                                        AskAgain = false;
                                        Console.WriteLine("HyperSpace Cheese Battle\n");
                                        BoardDraw();
                                        Console.WriteLine("\n{0} chose to shoot another player", players[player].Name);
                                        Console.Write("Which player do you want to shoot? (1, 2, 3 or 4): ");
                                        ShootChoice = int.Parse(Console.ReadLine());
                                        System.Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("HyperSpace Cheese Battle\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        if (ShootChoice > NoPlayers || ShootChoice < 1 || ShootChoice == players[player].PlayerNo)
                                        {
                                            Console.WriteLine("That input is invalid");
                                            AskAgain = true;
                                        }
                                    } while (AskAgain == true);
                                    bool AskAgain2;
                                    do
                                    {
                                        AskAgain2 = false;
                                        BoardDraw();
                                        Console.WriteLine("\n{0} chose to shoot {1}", players[player].Name, players[ShootChoice - 1].Name);
                                        players[ShootChoice - 1].Y = 0;
                                        Console.WriteLine("{0}, please enter an X value for your rocket to crash land on", players[ShootChoice - 1].Name);
                                        int CrashChoice = int.Parse(Console.ReadLine());
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("HyperSpace Cheese Battle\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        players[ShootChoice - 1].X = CrashChoice;
                                        if (CrashChoice > 7 || CrashChoice < 0)
                                        {
                                            Console.WriteLine("That input is invalid");
                                            AskAgain2 = true;
                                        }
                                        Console.WriteLine("{0}'s new coordinates are ({1},{2})", players[ShootChoice - 1].Name, CrashChoice, 0);
                                    } while (AskAgain2 == true);
                                }
                                catch
                                {
                                    Console.WriteLine("That input is invalid");
                                    AskAgain3 = true;
                                }
                            } while (AskAgain3 == true);
                            break;
                        default:
                            Console.WriteLine("That input is invalid");
                            AskAgain1 = true;
                            break;
                    }
                } while (AskAgain1 == true);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            string PlayAgain;
            do
            {
                #region Starting The Game
                // This is the beginning of the game
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("--------------------Welcome To HyperSpace Cheese Battle!--------------------");
                Console.WriteLine("---------------------------Created By Armin Pudic---------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n                             _----.");
                Console.WriteLine("                          .--      --.");
                Console.WriteLine("                         |----..      '-.");
                Console.WriteLine("                         |      '---..   '-.");
                Console.WriteLine("                         |.-. .-'.    ''--..'."); // Some fancy cheese ASCII art...
                Console.WriteLine("                         |'.'  -_'  .-.      |");
                Console.WriteLine("                         |      .-. '.-'   .-'");
                Console.WriteLine("                         '--..  '.'    .-  -.");
                Console.WriteLine("                              ''--..   '_'   :");
                Console.WriteLine("                                    ''--..   |");
                Console.WriteLine("                                          ''-'");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("                                __");
                Console.WriteLine(@"                                \ \_____");
                Console.WriteLine(@"################################[==_____> _ - _ - _ - _ - _ - _ - _ - _ -");     // Rockets too!
                Console.WriteLine("                                /_/      __");
                Console.WriteLine(@"                                         \ \_____");
                Console.WriteLine(@"   ######################################[==_____> _ - _ - _ - _ - _ - _ - _ -");
                Console.WriteLine("                                         /_/");
                Console.ForegroundColor = ConsoleColor.White;
                bool AskAgain2;
                AskAgain2 = false;
                do
                {
                    Console.Write("\nRun in testing mode? (Y/N) "); // The choice to check see if the 'TestingMode' method should be run
                    string TestingChoice = Console.ReadLine();
                    TestingChoice.ToUpper();
                    if (TestingChoice == "y")
                    {
                        TestingMode = true;
                    }
                    if (TestingChoice == "n")
                    {
                        break;
                    }
                    if (TestingChoice != "y" && TestingChoice != "n")
                    {
                        Console.WriteLine("That input is invalid");
                        AskAgain2 = true;
                    }
                } while (AskAgain2 == true);
                bool AskAgain;
                do
                {
                    AskAgain = false;
                    try
                    {
                        Console.Write("\nPlease enter the number of players from (2 to 4) in digits: ");
                        NoPlayers = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        AskAgain = true;
                    }
                    if (NoPlayers < 2 || NoPlayers > 4)
                    {
                        Console.WriteLine("That input is invalid");
                        AskAgain = true;
                    }
                } while (AskAgain == true);
                players = new Player[NoPlayers];
                System.Console.Clear();
                for (int i = 0; i < NoPlayers; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("HyperSpace Cheese Battle");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("            __");
                    Console.WriteLine(@"            \ \_____");
                    Console.WriteLine(@" ###########[==_____>");
                    Console.WriteLine("            /_/      __");
                    Console.WriteLine(@"                     \ \_____");
                    Console.WriteLine(@"        #############[==_____>");
                    Console.WriteLine("                     /_/");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nPlease enter the name of player {0}: ", i + 1);
                    players[i].Name = Console.ReadLine(); // This is storing all the player names into the 'players' array and assigning them the default X and Y values and a player number
                    players[i].X = 0;
                    players[i].Y = 0;
                    players[i].PlayerNo = i + 1;
                    switch (i) // This allocates a colour to each player
                    {
                        case 0:
                            players[i].PlayerColour = ConsoleColor.Blue;
                            break;
                        case 1:
                            players[i].PlayerColour = ConsoleColor.Red;
                            break;
                        case 2:
                            players[i].PlayerColour = ConsoleColor.Yellow;
                            break;
                        case 3:
                            players[i].PlayerColour = ConsoleColor.Green;
                            break;
                    }
                    System.Console.Clear();
                }
                #endregion

                #region Playing The Game
                // This is when the game is being played
                int TurnCounter = -1;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("HyperSpace Cheese Battle\n");
                Console.ForegroundColor = ConsoleColor.White;
                do
                {
                    TurnCounter++;
                    if (TurnCounter > NoPlayers - 1)
                    {
                        TurnCounter = 0;
                    }
                    BoardDraw();
                    Console.WriteLine("\nIt's {0}'s turn (Player {1}) (Your colour is {2})", players[TurnCounter].Name, players[TurnCounter].PlayerNo, players[TurnCounter].PlayerColour);
                    Console.Write("Press enter to roll");
                    Console.ReadLine();
                    System.Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("HyperSpace Cheese Battle\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    PlayerTurn(TurnCounter); // The 'PlayerTurn' method is being called here to start this players turn
                    if (players[TurnCounter].X == 7 && players[TurnCounter].Y == 7) // This is a checker that runs at the end of each players turn to see if anyone has won yet and if so then to end the game
                    {
                        won = true;
                    }
                    Console.WriteLine("{0}'s new coordinates are ({1},{2})", players[TurnCounter].Name, players[TurnCounter].X, players[TurnCounter].Y);
                } while (won == false);
                #endregion

                #region Ending The Game
                //This is the ending of the game
                bool AskAgain1;
                do
                {
                    AskAgain1 = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("{0} has won the game!", players[TurnCounter].Name);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n         _----.");
                    Console.WriteLine("      .--      --.");
                    Console.WriteLine("     |----..      '-.");
                    Console.WriteLine("     |      '---..   '-.");
                    Console.WriteLine("     |.-. .-'.    ''--..'.");
                    Console.WriteLine("     |'.'  -_'  .-.      |");
                    Console.WriteLine("     |      .-. '.-'   .-'");
                    Console.WriteLine("     '--..  '.'    .-  -.");
                    Console.WriteLine("          ''--..   '_'   :");
                    Console.WriteLine("                ''--..   |");
                    Console.WriteLine("                      ''-'");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("            __");
                    Console.WriteLine(@"            \ \_____");
                    Console.WriteLine(@" ###########[==_____>");
                    Console.WriteLine("            /_/      __");
                    Console.WriteLine(@"                     \ \_____");
                    Console.WriteLine(@"        #############[==_____>");
                    Console.WriteLine("                     /_/");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nDo you want to play again? (Y/N): "); // This part checks to see if the user wants to play again
                    PlayAgain = Console.ReadLine();
                    PlayAgain.ToUpper();
                    if (PlayAgain == "n")
                    {
                        Environment.Exit(0);
                    }
                    if (PlayAgain != "y" && PlayAgain != "n")
                    {
                        Console.WriteLine("That input is invalid");
                        AskAgain1 = true;
                    }
                    if (PlayAgain == "y")
                    {
                        won = false;
                        TestingMode = false;
                        Console.Clear();
                        break;
                    }
                } while (AskAgain1 == true);
                #endregion
            } while (PlayAgain == "y");
        }
    }
}
