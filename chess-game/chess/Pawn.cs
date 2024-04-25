// Purpose: Pawn class implementation. Represents a Pawn piece in a chess game.
// The Pawn class is a subclass of the Piece class. It is used
// to represent a Pawn piece in a chess game. It contains a constructor
// that initializes the Pawn's color and the board it belongs to.
// The Pawn class does not contain any additional methods or properties.
// The Pawn class overrides the ToString method to return the string "P".
// The Pawn class is used to represent a Pawn piece in a chess game.
// The Pawn class is a subclass of the Piece class.
// The Pawn class contains a constructor that initializes the Pawn's color and the board it belongs to.
// The Pawn class overrides the ToString method to return the string "P".
// The Pawn class does not contain any additional methods or properties.
namespace chess_game.chess
{
    public class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "P";
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
        public override bool[,] PossibleMove() // Method to check if a move is possible for a Pawn piece on the board or not
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            // Above
            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column);
            }
            else
            {
                position.SetValues(Position.Row + 1, Position.Column);
            }
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // NorthEast
            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column + 1);
            }
            else
            {
                position.SetValues(Position.Row + 1, Position.Column + 1);
            }
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            // NorthWest
            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column - 1);
            }
            else
            {
                position.SetValues(Position.Row + 1, Position.Column - 1);
            }
            if (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            return matrix;
        }
    }
}