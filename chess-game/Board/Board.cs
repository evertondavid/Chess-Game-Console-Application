using chess_game;
using System;

namespace chess_game
{
    /// <summary>
    /// Represents the chess board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The number of rows on the board.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// The number of columns on the board.
        /// </summary>
        public int Columns { get; }

        private Piece[,] Pieces;

        /// <summary>
        /// Constructor to create a board with a given number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows on the board.</param>
        /// <param name="columns">The number of columns on the board.</param>
        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        /// <summary>
        /// Gets the piece at the specified position on the board.
        /// </summary>
        /// <param name="row">The row of the position.</param>
        /// <param name="column">The column of the position.</param>
        /// <returns>The piece at the specified position.</returns>
        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        /// <summary>
        /// Gets the piece at the specified position on the board.
        /// </summary>
        /// <param name="position">The position to get the piece from.</param>
        /// <returns>The piece at the specified position.</returns>
        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        /// <summary>
        /// Checks if a piece exists at the specified position on the board.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if a piece exists at the specified position; otherwise, false.</returns>
        public bool PieceExists(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        /// <summary>
        /// Puts a piece on the board at the specified position.
        /// </summary>
        /// <param name="piece">The piece to put on the board.</param>
        /// <param name="position">The position to put the piece.</param>
        public void PutPiece(Piece piece, Position position)
        {
            if (PieceExists(position))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        /// <summary>
        /// Removes a piece from the board at the specified position.
        /// </summary>
        /// <param name="position">The position to remove the piece from.</param>
        /// <returns>The piece removed from the board.</returns>
        public Piece? RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece aux = Piece(position); // Store the piece in a variable to return it later
            aux.Position = null;
            Pieces[position.Row, position.Column] = null;
            return aux;
        }

        /// <summary>
        /// Checks if a position is valid on the board (inside the board) or not.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is valid; otherwise, false.</returns>
        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates a position on the board (inside the board) or not.
        /// </summary>
        /// <param name="position">The position to validate.</param>
        /// <exception cref="BoardException">Thrown when the position is invalid.</exception>
        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
