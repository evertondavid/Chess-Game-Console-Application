using System;

namespace chess_game.chess
{
    /// <summary>
    /// Represents a Pawn piece in a chess game.
    /// </summary>
    public class Pawn : Piece
    {
        private readonly ChessMatch _chessMatch; // Property to store the ChessMatch object

        /// <summary>
        /// Initializes a new instance of the <see cref="Pawn"/> class with the specified board, color, and chess match.
        /// </summary>
        /// <param name="board">The board the pawn belongs to.</param>
        /// <param name="color">The color of the pawn.</param>
        /// <param name="chessMatch">The chess match the pawn belongs to.</param>
        public Pawn(Board board, Color color, ChessMatch chessMatch) : base(board, color, color == Color.White ? "♙" : "♟")
        {
            _chessMatch = chessMatch;
        }

        /// <summary>
        /// Returns the string representation of the pawn piece.
        /// </summary>
        /// <returns>The symbol representing the pawn piece.</returns>
        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>
        /// Determines the possible moves for the pawn piece on the board.
        /// </summary>
        /// <returns>A matrix indicating the possible moves for the pawn piece.</returns>
        public override bool[,] PossibleMove()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);
            int direction = (Color == Color.White) ? -1 : 1;

            // Forward move
            position.SetValues(Position.Row + direction, Position.Column);
            if (Board.ValidPosition(position) && Board.Piece(position) == null)
            {
                matrix[position.Row, position.Column] = true;

                // Double move from initial position
                position.SetValues(Position.Row + 2 * direction, Position.Column);
                if (Board.ValidPosition(position) && Board.Piece(position) == null && MoveCount == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
            }

            // Diagonal captures
            position.SetValues(Position.Row + direction, Position.Column - 1);
            if (Board.ValidPosition(position) && ThereIsEnemy(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetValues(Position.Row + direction, Position.Column + 1);
            if (Board.ValidPosition(position) && ThereIsEnemy(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // #SpecialMove En Passant
            if (Position.Row == 3 || Position.Row == 4) // White pawn at row 3 or black pawn at row 4
            {
                position.SetValues(Position.Row, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position) && Board.Piece(position) == _chessMatch.EnPassantVulnerable)
                {
                    matrix[position.Row + direction, position.Column] = true;
                }
                position.SetValues(Position.Row, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsEnemy(position) && Board.Piece(position) == _chessMatch.EnPassantVulnerable)
                {
                    matrix[position.Row + direction, position.Column] = true;
                }
            }

            return matrix;
        }

        /// <summary>
        /// Checks if there is an enemy piece in the specified position on the board.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if there is an enemy piece at the position, otherwise false.</returns>
        private bool ThereIsEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }
    }
}
