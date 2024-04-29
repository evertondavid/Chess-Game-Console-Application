// Purpose: Rook class implementation. Represents a Rook piece in a chess game.
// The Rook class is a subclass of the Piece class. It is used
// to represent a Rook piece in a chess game. It contains a constructor
// that initializes the Rook's color and the board it belongs to.
// The Rook class does not contain any additional methods or properties.
namespace chess_game.chess
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color, color == Color.White ? "♖" : "♜")
        {
        }
        public override string ToString() // Method to return the string "R"
        {
            return Symbol;
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
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row = position.Row - 1;
            }
            // Right
            position.SetValues(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column + 1;
            }
            // Below
            position.SetValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row = position.Row + 1;
            }
            // Left
            position.SetValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column - 1;
            }
            return matrix;
        }
    }
}