using System;

namespace chess_game.chess
{
    /// <summary>
    /// Represents a Knight piece in a chess game.
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Knight"/> class with the specified color and board.
        /// </summary>
        /// <param name="board">The board the knight belongs to.</param>
        /// <param name="color">The color of the knight.</param>
        public Knight(Board board, Color color) : base(board, color, color == Color.White ? "♘" : "♞")
        {
        }

        /// <summary>
        /// Returns the string representation of the knight piece.
        /// </summary>
        /// <returns>The symbol representing the knight piece.</returns>
        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>
        /// Determines if the knight can move to the specified position on the board.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the knight can move to the specified position, otherwise false.</returns>
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        /// <summary>
        /// Determines the possible moves for the knight piece on the board.
        /// </summary>
        /// <returns>A matrix indicating the possible moves for the knight piece.</returns>
        /// <summary>
        /// Determines the possible moves for the knight piece on the board.
        /// </summary>
        /// <returns>A matrix indicating the possible moves for the knight piece.</returns>
        public override bool[,] PossibleMove()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            int[][] moves = { new int[] { -1, -2 }, new int[] { -2, -1 }, new int[] { -2, 1 }, new int[] { -1, 2 },
                              new int[] { 1, 2 }, new int[] { 2, 1 }, new int[] { 2, -1 }, new int[] { 1, -2 } };

            for (int i = 0; i < moves.Length; i++)
            {
                int row = Position.Row + moves[i][0];
                int col = Position.Column + moves[i][1];
                Position newPosition = new Position(row, col);

                if (Board.ValidPosition(newPosition) && CanMove(newPosition))
                {
                    matrix[row, col] = true;
                }
            }

            return matrix;
        }
    }
}