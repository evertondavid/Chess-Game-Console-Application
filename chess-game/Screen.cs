using System;
using chess_game.chess;

namespace chess_game

{
    public class Screen
    {
        public static void PrintBoard(Board board) // Method to print the board on the screen
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " "); // Print the row number
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
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
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }


    }
}