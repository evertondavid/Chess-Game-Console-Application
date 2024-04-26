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
        public Pawn(Board board, Color color) : base(board, color) // Constructor
        {
        }
        public override string ToString() // Method to return the string "P"
        {
            return "P";
        }
        private bool CanMove(Position position) // Method to check if a Pawn piece can move to a position on the board or not
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
        public override bool Equals(object? obj) // Method to check if two Pawn pieces are equal or not
        {
            return base.Equals(obj);
        }
        public override bool[,] PossibleMove() // Method to check if a move is possible for a Pawn piece on the board or not
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            //Above
            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position) && MoveCount == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
            }
            else // Below
            {
                position.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position) && MoveCount == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
            }
            return matrix;
        }
    }
}