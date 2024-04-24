// Purpose: King class implementation. Represents a King piece in a chess game.
// The King class is a subclass of the Piece class. It is used
// to represent a King piece in a chess game. It contains a constructor
// that initializes the King's color and the board it belongs to.
// The King class does not contain any additional methods or properties.
namespace chess_game.chess
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "K";
        }
    }
}