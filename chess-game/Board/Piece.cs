namespace chess_game;
public abstract class Piece
{
    public Position? Position { get; set; }
    public Color Color { get; protected set; }
    public int MoveCount { get; protected set; }
    public Board? Board { get; protected set; }

    public Piece(Board board, Color color) // Constructor to create a piece with a given board and color
    {
        Position = null;
        Board = board;
        Color = color;
        MoveCount = 0;
    }

    public void IncrementMoveCount() // Method to increment the move count of a piece by 1 when it moves
    {
        MoveCount++;
    }
    public void DecrementMoveCount() // Method to decrement the move count of a piece by 1 when it moves
    {
        MoveCount--;
    }
    public abstract bool[,] PossibleMove(); // Method to check if a move is possible for a piece
    public bool PossibleMove(Position position) // Method to check if a move is possible for a piece
    {
        return PossibleMove()[position.Row, position.Column];
    }
    public bool IsThereAnyPossibleMove() // Method to check if there is any possible move for a piece
    {
        bool[,] mat = PossibleMove();
        for (int i = 0; i < Board.Rows; i++)
        {
            for (int j = 0; j < Board.Columns; j++)
            {
                if (mat[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
