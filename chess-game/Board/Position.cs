using System;

namespace chess_game
{
    /// <summary>
    /// Represents a position on the chessboard.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Gets or sets the row of the position.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column of the position.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class with the specified row and column.
        /// </summary>
        /// <param name="row">The row of the position.</param>
        /// <param name="column">The column of the position.</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Sets the row and column values of the position.
        /// </summary>
        /// <param name="row">The new row value.</param>
        /// <param name="column">The new column value.</param>
        public void SetValues(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Sets the row and column values of the position using another position.
        /// </summary>
        /// <param name="position">The position containing the new row and column values.</param>
        public void SetValues(Position position)
        {
            Row = position.Row;
            Column = position.Column;
        }

        /// <summary>
        /// Returns a string representation of the position.
        /// </summary>
        /// <returns>A string representation of the position.</returns>
        public override string ToString()
        {
            return $"{Row}, {Column}";
        }
    }
}
