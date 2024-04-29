<<<<<<< HEAD
=======
using System;

>>>>>>> origin/main
namespace chess_game.chess
{
    /// <summary>
    /// Represents a position on the chessboard using chess notation.
    /// </summary>
    public class ChessPosition
    {
        /// <summary>
        /// Gets or sets the column of the chess position.
        /// </summary>
        public char Column { get; set; }

        /// <summary>
        /// Gets or sets the row of the chess position.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessPosition"/> class with the specified column and row.
        /// </summary>
        /// <param name="column">The column of the chess position.</param>
        /// <param name="row">The row of the chess position.</param>
        public ChessPosition(char column, int row)
        {
            if (!IsValidColumn(column))
            {
                throw new ArgumentException("Invalid column. Columns must be between 'a' and 'h'.", nameof(column));
            }

            if (!IsValidRow(row))
            {
                throw new ArgumentException("Invalid row. Rows must be between 1 and 8.", nameof(row));
            }

            Column = column;
            Row = row;
        }

        /// <summary>
        /// Converts the chess position to a board position.
        /// </summary>
        /// <returns>The corresponding board position.</returns>
        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        /// <summary>
        /// Overrides the ToString method to return the chess position in string format.
        /// </summary>
        /// <returns>The string representation of the chess position.</returns>
        public override string ToString()
        {
            return $"{Column}{Row}";
        }

        /// <summary>
        /// Checks if the specified column is valid.
        /// </summary>
        /// <param name="column">The column to validate.</param>
        /// <returns>True if the column is valid, otherwise false.</returns>
        private bool IsValidColumn(char column)
        {
            return column >= 'a' && column <= 'h';
        }

        /// <summary>
        /// Checks if the specified row is valid.
        /// </summary>
        /// <param name="row">The row to validate.</param>
        /// <returns>True if the row is valid, otherwise false.</returns>
        private bool IsValidRow(int row)
        {
            return row >= 1 && row <= 8;
        }
    }
}
