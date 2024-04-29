using System;

namespace chess_game.chess
{
    /// <summary>
    /// Represents a Bishop piece in a chess game.
    /// </summary>
    public class Bishop : Piece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bishop"/> class with the specified color and board.
        /// </summary>
        /// <param name="board">The board the bishop belongs to.</param>
        /// <param name="color">The color of the bishop.</param>
        public Bishop(Board board, Color color) : base(board, color, color == Color.White ? "♗" : "♝")
        {
        }

        /// <summary>
        /// Returns the string representation of the bishop piece.
        /// </summary>
        /// <returns>The symbol representing the bishop piece.</returns>
        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>
        /// Determines if the bishop can move to the specified position on the board.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the bishop can move to the specified position, otherwise false.</returns>
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        /// <summary>
        /// Determines the possible moves for the bishop piece on the board.
        /// </summary>
        /// <returns>A matrix indicating the possible moves for the bishop piece.</returns>
        public override bool[,] PossibleMove()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            // Northwest
            position.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column - 1);
            }

            // Northeast
            position.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column + 1);
            }

            // Southeast
            position.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column + 1);
            }

            // Southwest
            position.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column - 1);
            }

            return matrix;
        }
    }
}
