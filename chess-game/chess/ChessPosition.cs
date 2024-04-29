namespace chess_game.chess
{
    public class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row) // Constructor 
        {
            Column = column;
            Row = row;
        }
        public Position ToPosition() // Convert ChessPosition to Position 
        {
            return new Position(8 - Row, Column - 'a');
        }
        public override string ToString() // Override ToString method
        {
            return "" + Column + Row;
        }
    }
}