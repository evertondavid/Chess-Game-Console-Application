namespace chess_game
{
    public class Screen
    {
        public static void PrintBoard(Board board) // Method to print the board on the screen
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        System.Console.Write(board.Piece(i, j) + " ");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine("a b c d e f g h");
        }
    }
}