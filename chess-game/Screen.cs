using System;
using System.Collections.Generic;
using chess_game.chess;
namespace chess_game
{
    public class Screen
    {
        public static void PrintMatch(ChessMatch match) // Method to print the match on the screen
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            if (!match.Checkmate)
            {}
            System.Console.WriteLine("Waiting move: " + match.CurrentPlayer); // será deletado em breve!!!
            if (!match.Checkmate)
            {
                Console.WriteLine("Waiting player: " + match.CurrentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.CurrentPlayer);
            }
        }
        public static void PrintCapturedPieces(ChessMatch match) // Method to print the captured pieces on the screen
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            PrintHashSet(match.GetCapturedPieces(Color.White)); // Validar se o método GetCapturedPieces está correto
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintHashSet(match.GetCapturedPieces(Color.Black)); // Validar se o método GetCapturedPieces está correto
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void PrintHashSet(HashSet<Piece> set) // Method to print a hash set of pieces on the screen
        {
            Console.Write("[");
            foreach (Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void PrintBoard(Board board) // Method to print the board on the screen
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " "); // Print the row number
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintBoard(Board board, bool[,] possibleMoves) // Method to print the board on the screen
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " "); // Print the row number
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }
        public static ChessPosition ReadChessPosition() // Method to read a chess position from the user
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }
        public static void PrintPiece(Piece piece) // Method to print a piece on the screen
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                System.Console.Write(" ");
            }
        }
    }
}