using System;
using chess_game;
namespace chess_game.chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Checkmate { get; private set; }
        public ChessMatch() // Constructor to create a new chess match
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Checkmate = false;
            PlacePieces();
        }
        public void ExecuteMove(Position origin, Position destination) // Method to execute a move on the board
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(piece, destination);
        }
        public void PerformMove(Position origin, Position destination) // Method to perform a move on the board
        {
            ExecuteMove(origin, destination);
            Turn++;
            ChangePlayer();
        }
        public void ValidateOriginPosition(Position position) // Method to validate the origin position
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("There is no piece on the chosen position!");
            }
            if (CurrentPlayer != Board.Piece(position).Color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }
            if (!Board.Piece(position).IsThereAnyPossibleMove())
            {
                throw new BoardException("There are no possible moves for the chosen piece!");
            }
        }
        public void ValidateDestinationPosition(Position origin, Position destination) // Method to validate the destination position
        {
            if (!Board.Piece(origin).PossibleMove(destination))
            {
                throw new BoardException("Invalid destination position!");
            }
        }
        private void ChangePlayer() // Method to change the player
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
        private void PlacePieces() // Method to place the pieces on the board
        {
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}