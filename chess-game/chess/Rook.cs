using System;

namespace chess_game.chess
{
    /// <summary>
    /// Represents a Rook piece in a chess game.
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rook"/> class with the specified board and color.
        /// </summary>
        /// <param name="board">The board the rook belongs to.</param>
        /// <param name="color">The color of the rook.</param>
        public Rook(Board board, Color color) : base(board, color, color == Color.White ? "♖" : "♜")
        {
        }

        /// <summary>
        /// Returns the string representation of the rook piece.
        /// </summary>
        /// <returns>The symbol representing the rook piece.</returns>
        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>
        /// Determines the possible moves for the rook piece on the board.
        /// </summary>
        /// <returns>A matrix indicating the possible moves for the rook piece.</returns>
        public override bool[,] PossibleMove()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            PieceMoveHelper(-1, 0, matrix); // North
            PieceMoveHelper(0, 1, matrix); // East
            PieceMoveHelper(1, 0, matrix); // South
            PieceMoveHelper(0, -1, matrix); // West

            return matrix;
        }

        /// <summary>
        /// Checks if a move is possible in a given direction and updates the move matrix.
        /// </summary>
        /// <param name="deltaRow">The change in row for the direction.</param>
        /// <param name="deltaColumn">The change in column for the direction.</param>
        /// <param name="matrix">The move matrix to update.</param>
        private void PieceMoveHelper(int deltaRow, int deltaColumn, bool[,] matrix)
        {
            Position position = new Position(Position.Row, Position.Column);
            while (true)
            {
                position.SetValues(position.Row + deltaRow, position.Column + deltaColumn);
                if (!Board.ValidPosition(position))
                {
                    break;
                }
                Piece piece = Board.Piece(position);
                if (piece != null)
                {
                    if (piece.Color != Color)
                    {
                        matrix[position.Row, position.Column] = true;
                    }
                    break;
                }
                matrix[position.Row, position.Column] = true;
            }
        }
    }
}
