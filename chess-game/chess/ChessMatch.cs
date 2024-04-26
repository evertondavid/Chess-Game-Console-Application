using System;
using System.Collections.Generic;
using chess_game;
namespace chess_game.chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Checkmate { get; private set; }
        private HashSet<Piece> Pieces; // HashSet to store the pieces in the match 
        private HashSet<Piece> CapturedPieces; // HashSet to store the captured pieces in the match
        public bool Check { get; private set; }
        public Piece EnPassantVulnerable { get; private set; }

        public ChessMatch() // Constructor to create a new chess match
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Checkmate = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            Check = false;
            EnPassantVulnerable = null;
            PlacePieces();
        }
        public Piece ExecuteMove(Position origin, Position destination) // Method to execute a move on the board
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(piece, destination);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            // Castling
            // Short castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Row, origin.Column + 3);
                Position destinationRook = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(originRook);
                rook.IncrementMoveCount();
                Board.PutPiece(rook, destinationRook);
            }
            // Long castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Row, origin.Column - 4);
                Position destinationRook = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(originRook);
                rook.IncrementMoveCount();
                Board.PutPiece(rook, destinationRook);
            }
            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(destination.Row + 1, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destination.Row - 1, destination.Column);
                    }
                    capturedPiece = Board.RemovePiece(pawnPosition);
                    CapturedPieces.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }
        public void UndoMove(Position origin, Position destination, Piece capturedPiece) // Method to undo a move on the board
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecrementMoveCount();
            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destination);
                CapturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(piece, origin);
            // Castling
            // Short castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Row, origin.Column + 3);
                Position destinationRook = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(destinationRook);
                rook.DecrementMoveCount();
                Board.PutPiece(rook, originRook);
            }
            // Long castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Row, origin.Column - 4);
                Position destinationRook = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(destinationRook);
                rook.DecrementMoveCount();
                Board.PutPiece(rook, originRook);
            }
            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == EnPassantVulnerable)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destination.Column);
                    }
                    Board.PutPiece(pawn, pawnPosition);
                }
            }
        }
        public void PerformMove(Position origin, Position destination) // Method to perform a move on the board
        {
            Piece capturedPiece = ExecuteMove(origin, destination);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (TestCheckmate(Opponent(CurrentPlayer)))
            {
                Checkmate = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
            // En passant
            Piece piece = Board.Piece(destination);
            if (piece is Pawn && (destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2))
            {
                EnPassantVulnerable = piece;
            }
            else
            {
                EnPassantVulnerable = null;
            }
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
        public HashSet<Piece> GetCapturedPieces(Color color) // Method to get the captured pieces of a given color
        {
            HashSet<Piece> capturedPieces = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    capturedPieces.Add(piece);
                }
            }
            return capturedPieces;
        }
        public HashSet<Piece> GetPiecesInPlay(Color color) // Method to get the pieces in play of a given color
        {
            HashSet<Piece> piecesInPlay = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    piecesInPlay.Add(piece);
                }
            }
            piecesInPlay.ExceptWith(GetCapturedPieces(color));
            return piecesInPlay;
        }
        private Color Opponent(Color color) // Method to get the opponent of a given color
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece King(Color color) // Method to get the king of a given color 
        {
            foreach (Piece piece in GetPiecesInPlay(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
        public bool IsInCheck(Color color) // Method to check if a given color is in check
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException($"There is no {color} king on the board!");
            }
            foreach (Piece piece in GetPiecesInPlay(Opponent(color)))
            {
                bool[,] mat = piece.PossibleMove();
                if (mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }
        public bool TestCheckmate(Color color) // Method to test if a given color is in checkmate
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece piece in GetPiecesInPlay(color))
            {
                bool[,] mat = piece.PossibleMove();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destination);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void PlaceNewPiece(char column, int row, Piece piece) // Method to place a new piece on the board
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        private void PlacePieces() // Method to place the pieces on the board
        {
            // Checkmate test
            /*
            PlaceNewPiece('c', 1, new Rook(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('h', 7, new Rook(Board, Color.White));
            PlaceNewPiece('a', 8, new King(Board, Color.Black));
            PlaceNewPiece('b', 8, new Rook(Board, Color.Black));
            */
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}