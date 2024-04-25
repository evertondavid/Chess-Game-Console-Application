using System;
namespace chess_game;

public class BoardException : Exception
{
    public BoardException(string message) : base(message)
    {
    }

}
