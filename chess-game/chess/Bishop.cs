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
        public Bishop(Board board, Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "B";
        }
    }
}