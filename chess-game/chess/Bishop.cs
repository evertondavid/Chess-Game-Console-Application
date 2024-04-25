// Purpose: Bishop class implementation. Represents a Bishop piece in a chess game.
// The Bishop class is a subclass of the Piece class. It is used
// to represent a Bishop piece in a chess game. It contains a constructor
// that initializes the Bishop's color and the board it belongs to.
// The Bishop class does not contain any additional methods or properties.
// The Bishop class overrides the ToString method to return the string "B".
namespace chess_game.chess
{
    public class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color) // Constructor to create a Bishop piece with a given board and color
        {
        }
        public override string ToString() // Method to return the string "B" when a Bishop piece is printed on the board
        {
            return "B";
        }
        private bool CanMove(Position position) // Method to check if a Bishop piece can move to a given position on the board or not
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
        public override bool Equals(object? obj) // Method to check if two Bishop pieces are the same or not
        {
            return base.Equals(obj);
        }
        public override bool[,] PossibleMove() // Method to check if a move is possible for a Bishop piece on the board or not
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            // Above
            position.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row = position.Row - 1;
                position.Column = position.Column - 1;
            }
            // NorthEast
            position.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column + 1);
            }
            // Right
            position.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column + 1);
            }
            // SouthEast
            position.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column - 1);
            }
            return matrix;
        }
    }
}