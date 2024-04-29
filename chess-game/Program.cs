using chess_game;
using chess_game.chess;
try
{
    ChessMatch match = new ChessMatch();
    System.Console.WriteLine();
    while (!match.Checkmate)
    {
        try
        {
            Console.Clear();
            Screen.PrintMatch(match);
            System.Console.WriteLine();
            System.Console.Write("Origin: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            match.ValidateOriginPosition(origin);
            bool[,] possibleMoves = match.Board.Piece(origin).PossibleMove();
            Console.Clear();  //  ok
            Screen.PrintBoard(match.Board, possibleMoves);
            System.Console.WriteLine();
            System.Console.Write("Destination: ");
            Position destination = Screen.ReadChessPosition().ToPosition();
            match.ValidateDestinationPosition(origin, destination);
            match.PerformMove(origin, destination);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
    Console.Clear();
    Screen.PrintMatch(match);
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
