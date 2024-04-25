// Purpose: King class implementation. Represents a King piece in a chess game.
// The King class is a subclass of the Piece class. It is used
// to represent a King piece in a chess game. It contains a constructor
// that initializes the King's color and the board it belongs to.
// The King class does not contain any additional methods or properties.
namespace chess_game.chess
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color) // Constructor to create a King piece with a given board and color
        {
        }
        public override string ToString() // Method to print a King piece on the board
        {
            return "K";
        }
        private bool CanMove(Position position) // Method to check if a King piece can move to a given position on the board or not
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
        public override bool Equals(object? obj) // Method to check if two King pieces are the same or not
        {
            return base.Equals(obj);
        }
        public override bool[,] PossibleMove() // Method to check if a move is possible for a King piece on the board or not 
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            // Above
            position.SetValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // NorthEast
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Right
            position.SetValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // SouthEast
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Below
            position.SetValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // SouthWest
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // Left
            position.SetValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // NorthWest
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            return matrix;
        }
    }
}