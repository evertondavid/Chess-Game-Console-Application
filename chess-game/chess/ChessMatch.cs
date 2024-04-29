namespace chess_game.chess
{
    /// <summary>
    /// Represents a chess match.
    /// </summary>
    public class ChessMatch
    {
        /// <summary>
        /// Gets the chess board.
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Gets the current turn number.
        /// </summary>
        public int Turn { get; private set; }

        /// <summary>
        /// Gets the color of the current player.
        /// </summary>
        public Color CurrentPlayer { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the match is in checkmate.
        /// </summary>
        public bool Checkmate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current player is in check.
        /// </summary>
        public bool Check { get; private set; }

        /// <summary>
        /// Gets the piece vulnerable to en passant.
        /// </summary>
        public Piece EnPassantVulnerable { get; private set; }

        private readonly HashSet<Piece> Pieces;
        private readonly HashSet<Piece> CapturedPieces;

        /// <summary>
        /// Initializes a new instance of the ChessMatch class.
        /// </summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="ChessMatch"/> class.
        /// </summary>
        public ChessMatch()
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
        /// <summary>
        /// Executes a move on the board.
        /// </summary>
        /// <param name="origin">The position of the piece to move.</param>
        /// <param name="destination">The destination position.</param>
        /// <returns>The captured piece, if any.</returns>
        public Piece ExecuteMove(Position origin, Position destination)
        {
            // Remove the piece from the original position
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoveCount();

            // Check for a captured piece
            Piece capturedPiece = Board.RemovePiece(destination);

            // Put the piece in the destination position
            Board.PutPiece(piece, destination);

            // Handle castling
            HandleCastling(piece, origin, destination);

            // Handle en passant
            HandleEnPassant(piece, origin, destination, capturedPiece);

            // Add the captured piece to the list
            if (capturedPiece != null)
            {
                AddCapturedPiece(capturedPiece);
            }

            return capturedPiece;
        }
        /// <summary>
        /// Handles castling, if applicable.
        /// </summary>
        /// <param name="piece">The piece being moved.</param>
        /// <param name="origin">The original position of the piece.</param>
        /// <param name="destination">The destination position of the piece.</param>
        private void HandleCastling(Piece piece, Position origin, Position destination)
        {
            if (piece is King && Math.Abs(destination.Column - origin.Column) == 2)
            {
                // Short castling
                if (destination.Column > origin.Column)
                {
                    Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                    Position rookDestination = new Position(origin.Row, origin.Column + 1);
                    MovePieceForCastling(rookOrigin, rookDestination);
                }
                // Long castling
                else
                {
                    Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                    Position rookDestination = new Position(origin.Row, origin.Column - 1);
                    MovePieceForCastling(rookOrigin, rookDestination);
                }
            }
        }
        /// <summary>
        /// Moves a piece for castling.
        /// </summary>
        /// <param name="origin">The original position of the piece.</param>
        /// <param name="destination">The destination position of the piece.</param>
        private void MovePieceForCastling(Position origin, Position destination)
        {
            Piece rook = Board.RemovePiece(origin);
            rook.IncrementMoveCount();
            Board.PutPiece(rook, destination);
        }
        /// <summary>
        /// Handles en passant capture, if applicable.
        /// </summary>
        /// <param name="piece">The piece being moved.</param>
        /// <param name="origin">The original position of the piece.</param>
        /// <param name="destination">The destination position of the piece.</param>
        /// <param name="capturedPiece">The piece that was captured, if any.</param>
        private void HandleEnPassant(Piece piece, Position origin, Position destination, Piece capturedPiece)
        {
            if (piece is Pawn && origin.Column != destination.Column && capturedPiece == null)
            {
                int pawnRowOffset = (piece.Color == Color.White) ? 1 : -1;
                Position enPassantPosition = new Position(destination.Row - pawnRowOffset, destination.Column);
                capturedPiece = Board.RemovePiece(enPassantPosition);
                if (capturedPiece != null)
                {
                    AddCapturedPiece(capturedPiece);
                }
            }
        }
        /// <summary>
        /// Adds a captured piece to the list of captured pieces.
        /// </summary>
        /// <param name="capturedPiece">The piece that was captured.</param>
        private void AddCapturedPiece(Piece capturedPiece)
        {
            CapturedPieces.Add(capturedPiece);
        }
        /// <summary>
        /// Undoes a move on the board.
        /// </summary>
        /// <param name="origin">The original position of the moved piece.</param>
        /// <param name="destination">The destination position of the moved piece.</param>
        /// <param name="capturedPiece">The captured piece, if any.</param>
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
        /// <summary>
        /// Performs a move on the board.
        /// </summary>
        /// <param name="origin">The position of the piece to move.</param>
        /// <param name="destination">The destination position.</param>
        public void PerformMove(Position origin, Position destination) // Method to perform a move on the board
        {

            Piece capturedPiece = ExecuteMove(origin, destination);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            // #SpecialMove Promotion
            Piece piece = Board.Piece(destination);
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destination.Row == 0) || (piece.Color == Color.Black && destination.Row == 7))
                {
                    piece = Board.RemovePiece(destination);
                    Pieces.Remove(piece);
                    // chosse the piece to promote between Queen, Rook, Bishop and Knight
                    System.Console.WriteLine("Enter the piece to promote (Q, R, B, N): ");
                    char promotion = char.Parse(Console.ReadLine());
                    Piece newPiece = null;
                    switch (promotion)
                    {
                        case 'Q':
                            newPiece = new Queen(Board, piece.Color);
                            break;
                        case 'R':
                            newPiece = new Rook(Board, piece.Color);
                            break;
                        case 'B':
                            newPiece = new Bishop(Board, piece.Color);
                            break;
                        case 'N':
                            newPiece = new Knight(Board, piece.Color);
                            break;
                        default:
                            newPiece = new Queen(Board, piece.Color);
                            break;
                    }
                    Board.PutPiece(newPiece, destination);
                    Pieces.Add(newPiece);

                    /*
                    Piece queen = new Queen(Board, piece.Color);
                    Board.PutPiece(queen, destination);
                    Pieces.Add(queen);
                    */
                }
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
            if (piece is Pawn && (destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2))
            {
                EnPassantVulnerable = piece;
            }
            else
            {
                EnPassantVulnerable = null;
            }
        }
        /// <summary>
        /// Validates the origin position of a move.
        /// </summary>
        /// <param name="position">The position to validate.</param>
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
        /// <summary>
        /// Validates the destination position of a move.
        /// </summary>
        /// <param name="origin">The origin position of the move.</param>
        /// <param name="destination">The destination position to validate.</param>
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
        /// <summary>
        /// Gets the captured pieces of a given color.
        /// </summary>
        /// <param name="color">The color of the pieces.</param>
        /// <returns>The captured pieces.</returns>
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
        /// <summary>
        /// Gets the pieces in play of a given color.
        /// </summary>
        /// <param name="color">The color of the pieces.</param>
        /// <returns>The pieces in play.</returns>
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
        /// <summary>
        /// Checks if a given color is in check.
        /// </summary>
        /// <param name="color">The color to check.</param>
        /// <returns>True if the color is in check, otherwise false.</returns>
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
        /// <summary>
        /// Checks if a given color is in checkmate.
        /// </summary>
        /// <param name="color">The color to check.</param>
        /// <returns>True if the color is in checkmate, otherwise false.</returns>
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
        /// <summary>
        /// Places a new piece on the board.
        /// </summary>
        /// <param name="column">The column letter (from 'a' to 'h').</param>
        /// <param name="row">The row number (from 1 to 8).</param>
        /// <param name="piece">The piece to place.</param>
        public void PlaceNewPiece(char column, int row, Piece piece) // Method to place a new piece on the board
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        /// <summary>
        /// Places the pieces on the board at the start of the game.
        /// </summary>
        private void PlacePieces()
        {
            // Place white pieces
            PlaceWhitePieces();

            // Place black pieces
            PlaceBlackPieces();
        }
        /// <summary>
        /// Places the white pieces on the board.
        /// </summary>
        private void PlaceWhitePieces()
        {
            PlaceRooks(Color.White);
            PlaceKnights(Color.White);
            PlaceBishops(Color.White);
            PlaceQueen(Color.White);
            PlaceKing(Color.White);
            PlacePawns(Color.White);
        }
        /// <summary>
        /// Places the black pieces on the board.
        /// </summary>
        private void PlaceBlackPieces()
        {
            PlaceRooks(Color.Black);
            PlaceKnights(Color.Black);
            PlaceBishops(Color.Black);
            PlaceQueen(Color.Black);
            PlaceKing(Color.Black);
            PlacePawns(Color.Black);
        }
        /// <summary>
        /// Places the rooks on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the rooks to place.</param>
        private void PlaceRooks(Color color)
        {
            PlaceNewPiece('a', color == Color.White ? 1 : 8, new Rook(Board, color));
            PlaceNewPiece('h', color == Color.White ? 1 : 8, new Rook(Board, color));
        }
        /// <summary>
        /// Places the knights on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the knights to place.</param>
        private void PlaceKnights(Color color)
        {
            PlaceNewPiece('b', color == Color.White ? 1 : 8, new Knight(Board, color));
            PlaceNewPiece('g', color == Color.White ? 1 : 8, new Knight(Board, color));
        }
        /// <summary>
        /// Places the bishops on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the bishops to place.</param>
        private void PlaceBishops(Color color)
        {
            PlaceNewPiece('c', color == Color.White ? 1 : 8, new Bishop(Board, color));
            PlaceNewPiece('f', color == Color.White ? 1 : 8, new Bishop(Board, color));
        }
        /// <summary>
        /// Places the queen on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the queen to place.</param>
        private void PlaceQueen(Color color)
        {
            PlaceNewPiece('d', color == Color.White ? 1 : 8, new Queen(Board, color));
        }

        /// <summary>
        /// Places the king on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the king to place.</param>
        private void PlaceKing(Color color)
        {
            PlaceNewPiece('e', color == Color.White ? 1 : 8, new King(Board, color, this));
        }
        /// <summary>
        /// Places the pawns on the board for the specified color.
        /// </summary>
        /// <param name="color">The color of the pawns to place.</param>
        private void PlacePawns(Color color)
        {
            int row = color == Color.White ? 2 : 7;
            for (char column = 'a'; column <= 'h'; column++)
            {
                PlaceNewPiece(column, row, new Pawn(Board, color, this));
            }
        }
    }
}