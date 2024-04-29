namespace chess_game;
public abstract class Piece
{
    /// <summary>
    /// The current position of the piece on the board. Null if the piece is not on the board.
    /// </summary>
    public Position? Position { get; set; }

    /// <summary>
    /// The color of the piece, determining the player it belongs to.
    /// </summary>
    public Color Color { get; protected set; }

    /// <summary>
    /// The number of moves this piece has made during the game.
    /// </summary>
    public int MoveCount { get; protected set; }

    /// <summary>
    /// The board to which this piece is assigned. Null if the piece is not currently on any board.
    /// </summary>
    public Board? Board { get; protected set; }

    /// <summary>
    /// A symbol representing the piece, used for visual representation in the UI.
    /// </summary>
    public string Symbol { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the Piece class.
    /// </summary>
    /// <param name="board">The board on which the piece will be placed.</param>
    /// <param name="color">The color indicating the player the piece belongs to.</param>
    /// <param name="symbol">The symbol representing the piece.</param>
    public Piece(Board board, Color color, string symbol)
    {
        Position = null;
        Board = board;
        Color = color;
        Symbol = symbol;
        MoveCount = 0;
    }

    /// <summary>
    /// Increments the move count of the piece by one. This should be called when a piece moves.
    /// </summary>
    public void IncrementMoveCount()
    {
        MoveCount++;
    }

    /// <summary>
    /// Decrements the move count of the piece by one. This should be called when undoing a piece's move.
    /// </summary>
    public void DecrementMoveCount()
    {
        MoveCount--;
    }

    /// <summary>
    /// Abstract method to determine possible moves for a piece. Must be implemented by derived classes.
    /// </summary>
    /// <returns>A 2D array of booleans indicating possible moves for this piece.</returns>
    public abstract bool[,] PossibleMove();

    /// <summary>
    /// Checks if a move to a specific position is possible.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the move is possible, otherwise false.</returns>
    public bool PossibleMove(Position position)
    {
        if (Board == null)
        {
            throw new InvalidOperationException("Board property is null.");
        }
        return PossibleMove()[position.Row, position.Column];
    }

    /// <summary>
    /// Determines whether there are any possible moves for the piece.
    /// </summary>
    /// <returns>True if there is at least one possible move for the piece, otherwise false.</returns>
    public bool IsThereAnyPossibleMove()
    {
        if (Board == null)
        {
            throw new InvalidOperationException("Board property is null.");
        }

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
