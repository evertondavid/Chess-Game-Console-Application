// Purpose: Knight class implementation. Represents a Knight piece in a chess game.
// The Knight class is a subclass of the Piece class. It is used
// to represent a Knight piece in a chess game. It contains a constructor
// that initializes the Knight's color and the board it belongs to.
// The Knight class does not contain any additional methods or properties.
// The Knight class overrides the ToString method to return the string "N".
namespace chess_game.chess
{
    public class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "N";
        }
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override bool[,] PossibleMove() // Method to check if a move is possible for a Knight piece on the board or not
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            // Above
            position.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            return matrix;
        }
    }
}