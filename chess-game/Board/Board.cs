namespace chess_game;
public class Board
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    private Piece[,] Pieces;
    public Board(int rows, int columns) // Constructor to create a board with a given number of rows and columns
    {
        Rows = rows;
        Columns = columns;
        Pieces = new Piece[rows, columns];
    }

    public Piece Piece(int row, int column) // Method to return a piece in a given position on the board
    {
        return Pieces[row, column];
    }
    public Piece Piece(Position position) // Method to return a piece in a given position on the board
    {
        return Pieces[position.Row, position.Column];
    }
    public bool PieceExists(Position position) // Method to check if there is a piece in a given position on the board
    {
        ValidatePosition(position);
        return Piece(position) != null;
    }
    public void PutPiece(Piece piece, Position position) // Method to put a piece on the board in a given position
    {
        if (PieceExists(position))
        {
            throw new BoardException("There is already a piece in that position!");
        }
        Pieces[position.Row, position.Column] = piece;
        piece.Position = position;
    }
    public Piece? RemovePiece(Position position) // Method to remove a piece from the board and return it to the caller
    {
        if (Piece(position) == null)
        {
            return null;
        }
        Piece aux = Piece(position); // Store the piece in a variable to return it later
        aux.Position = null;
        Pieces[position.Row, position.Column] = null;
        return aux;
    }
    public bool ValidPosition(Position position) // Method to check if a position is valid on the board (inside the board) or not
    {
        if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
        {
            return false;
        }
        return true;
    }
    public void ValidatePosition(Position position) // Method to validate a position on the board (inside the board) or not
    {
        if (!ValidPosition(position))
        {
            throw new BoardException("Invalid position!");
        }
    }
}
