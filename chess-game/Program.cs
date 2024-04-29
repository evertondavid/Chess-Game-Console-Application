using chess_game;
using chess_game.chess;
/// <summary>
/// Executes the chess game logic, handling exceptions and displaying the game state.
/// </summary>
try
{
    // Creates a new chess match instance.
    ChessMatch match = new ChessMatch();
    // Prints an empty line.
    System.Console.WriteLine();

    // Loops until the game is over (checkmate).
    while (!match.Checkmate)
    {
        try
        {
            // Clears the console screen.
            Console.Clear();
            // Prints the current state of the chess match.
            Screen.PrintMatch(match);
            // Prints an empty line.
            System.Console.WriteLine();
            // Prompts the user to input the origin position.
            System.Console.Write("Origin: ");
            // Reads and converts the origin position input by the user.
            Position origin = Screen.ReadChessPosition().ToPosition();
            // Validates the origin position entered by the user.
            match.ValidateOriginPosition(origin);
            // Calculates the possible moves for the piece at the specified origin position.
            bool[,] possibleMoves = match.Board.Piece(origin).PossibleMove();
            // Clears the console screen.
            Console.Clear();
            // Prints the board with highlighted possible moves.
            Screen.PrintBoard(match.Board, possibleMoves);
            // Prints an empty line.
            System.Console.WriteLine();
            // Prompts the user to input the destination position.
            System.Console.Write("Destination: ");
            // Reads and converts the destination position input by the user.
            Position destination = Screen.ReadChessPosition().ToPosition();
            // Validates the destination position entered by the user.
            match.ValidateDestinationPosition(origin, destination);
            // Performs the move on the chess board.
            match.PerformMove(origin, destination);
        }
        catch (BoardException e)
        {
            // Handles board-related exceptions by displaying the error message and waiting for user input.
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }

    // Clears the console screen.
    Console.Clear();
    // Prints the final state of the chess match.
    Screen.PrintMatch(match);
}
catch (BoardException e)
{
    // Handles board-related exceptions that occur during the initialization of the chess match.
    Console.WriteLine(e.Message);
}
