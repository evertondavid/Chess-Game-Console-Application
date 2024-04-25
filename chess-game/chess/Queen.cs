// Purpose: Queen class implementation. Represents a Queen piece in a chess game.
// The Queen class is a subclass of the Piece class. It is used
// to represent a Queen piece in a chess game. It contains a constructor
// that initializes the Queen's color and the board it belongs to.
// The Queen class does not contain any additional methods or properties.
// The Queen class overrides the ToString method to return the string "Q".

namespace chess_game.chess
{
    public class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "Q";
        }
    }
}