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
    }
}