// Purpose: Rook class implementation. Represents a Rook piece in a chess game.
// The Rook class is a subclass of the Piece class. It is used
// to represent a Rook piece in a chess game. It contains a constructor
// that initializes the Rook's color and the board it belongs to.
// The Rook class does not contain any additional methods or properties.

namespace chess_game.chess
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}