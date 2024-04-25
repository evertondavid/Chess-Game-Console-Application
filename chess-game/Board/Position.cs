namespace chess_game;
public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Position(int row, int column) // Constructor to create a position with a given row and column on the board
    {
        Row = row;
        Column = column;
    }
    public void SetValues(int row, int column) // Method to set the values of a position with a given row and column
    {
        Row = row;
        Column = column;
    }
    public void SetValues(Position position) // Method to set the values of a position with a given position
    {
        Row = position.Row;
        Column = position.Column;
    }
    public override string ToString() // Method to return the position as a string
    {
        return $"{Row}, {Column}";
    }
}
