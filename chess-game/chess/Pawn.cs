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
    }
}