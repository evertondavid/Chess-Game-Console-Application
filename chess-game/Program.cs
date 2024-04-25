using chess_game;
using chess_game.chess;
using System;
try
{
    ChessMatch match = new ChessMatch();
    System.Console.WriteLine();
    while (!match.Checkmate)
    {
        try
        {
            Console.Clear();
            Screen.PrintBoard(match.Board);
            System.Console.WriteLine();
            System.Console.Write("Origin: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            //bool[,] possibleMoves = match.Board.Piece(origin).PossibleMoves();
            //Console.Clear();
            //Screen.PrintBoard(match.Board, possibleMoves);
            //System.Console.WriteLine();
            System.Console.Write("Destination: ");
            Position destination = Screen.ReadChessPosition().ToPosition();
            match.ExecuteMove(origin, destination);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }



}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
