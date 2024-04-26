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
        private ChessMatch ChessMatch; // Property to store the ChessMatch object
        public Pawn(Board board, Color color, ChessMatch chessMatch) : base(board, color) // Constructor
        {
            ChessMatch = chessMatch;
        }
        public override string ToString() // Method to return the string "P"
        {
            return "P";
        }
        private bool ThereIsEnemy(Position position) // Method to check if there is an enemy piece in a given position on the board or not
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }
        private bool Free(Position position) // Method to check if a position on the board is free or not
        {
            return Board.Piece(position) == null;
        }
        public override bool[,] PossibleMove() // Method to check if a move is possible for a Pawn piece on the board or not
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(position) && Free(position) && MoveCount == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // #SpecialMove En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && ThereIsEnemy(left) && Board.Piece(left) == ChessMatch.EnPassantVulnerable)
                    {
                        matrix[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && ThereIsEnemy(right) && Board.Piece(right) == ChessMatch.EnPassantVulnerable)
                    {
                        matrix[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                position.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(position) && Free(position) && MoveCount == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // #SpecialMove En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && ThereIsEnemy(left) && Board.Piece(left) == ChessMatch.EnPassantVulnerable)
                    {
                        matrix[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && ThereIsEnemy(right) && Board.Piece(right) == ChessMatch.EnPassantVulnerable)
                    {
                        matrix[right.Row + 1, right.Column] = true;
                    }
                }
            }
            return matrix;
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
    }
}