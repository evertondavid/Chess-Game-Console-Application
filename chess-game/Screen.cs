using System;
using chess_game.chess;

namespace chess_game
{
    /// <summary>
    /// Provides methods to display the chess match on the console.
    /// </summary>
    public class Screen
    {
        private static readonly ConsoleColor BlackPieceColor = ConsoleColor.Yellow;

        /// <summary>
        /// Prints the current state of the chess match to the console.
        /// </summary>
        /// <param name="match">The chess match to be printed.</param>
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine("Waiting player: " + match.CurrentPlayer);

            if (match.Check)
            {
                Console.WriteLine(match.Checkmate ? "CHECKMATE!" : "CHECK!");
                if (match.Checkmate)
                {
                    Console.WriteLine("Winner: " + match.CurrentPlayer);
                }
            }
        }

        /// <summary>
        /// Prints the pieces captured during the game.
        /// </summary>
        /// <param name="match">The chess match containing the captured pieces.</param>
        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            PrintHashSet(match.GetCapturedPieces(Color.White));
            Console.WriteLine();

            Console.Write("Black: ");
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = BlackPieceColor;
            PrintHashSet(match.GetCapturedPieces(Color.Black));
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }

        /// <summary>
        /// Prints a set of pieces.
        /// </summary>
        /// <param name="set">The set of pieces to print.</param>
        private static void PrintHashSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        /// <summary>
        /// Prints the board to the console, highlighting possible moves.
        /// </summary>
        /// <param name="board">The board to print.</param>
        /// <param name="possibleMoves">A matrix indicating the possible moves for a selected piece.</param>
        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.Cyan;  // Brighter color for visibility

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
                        Console.BackgroundColor = ConsoleColor.Black; // Ensure a consistent dark background
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f g  h");
            Console.BackgroundColor = originalBackground;
        }

        /// <summary>
        /// Prints the board to the console.
        /// </summary>
        /// <param name="board">The board to print.</param>
        public static void PrintBoard(Board board)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor defaultBackground = ConsoleColor.Black;

            Console.BackgroundColor = defaultBackground;
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h  ");
            Console.BackgroundColor = originalBackground;
        }

        /// <summary>
        /// Prints a single chess piece on the console.
        /// </summary>
        /// <param name="piece">The piece to print.</param>
        private static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                ConsoleColor originalColor = Console.ForegroundColor;
                if (piece.Color == Color.Black)
                {
                    Console.ForegroundColor = BlackPieceColor;
                }
                Console.Write(piece.Symbol + " ");
                Console.ForegroundColor = originalColor;
            }
            Console.Write(" ");
        }

        /// <summary>
        /// Reads a chess position from the console input.
        /// </summary>
        /// <returns>The chess position entered by the user.</returns>
        public static ChessPosition ReadChessPosition()
        {
            while (true)
            {
                Console.WriteLine("Enter a position (e.g., 'e2'): ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(input) || input.Length != 2)
                {
                    throw new BoardException("Invalid input! Please enter a position like 'e2'.");
                }

                char column = input[0];
                if (column < 'a' || column > 'h' || !int.TryParse(input[1].ToString(), out int row) || row < 1 || row > 8)
                {
                    throw new BoardException("Invalid position! Column must be between 'a' and 'h' and row between 1 and 8.");
                }

                return new ChessPosition(column, row);
            }
        }
    }
}
