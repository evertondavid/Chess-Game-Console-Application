using System;
using chess_game;
namespace chess_game.chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Checkmate { get; private set; }
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Checkmate = false;
            PlacePieces();
        }
        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(piece, destination);
        }

        private void PlacePieces()
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