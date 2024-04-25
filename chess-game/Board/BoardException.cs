using System;
namespace chess_game;
public class BoardException : Exception
{
    public BoardException(string message) : base(message) // : base(message) -> repassando a mensagem para a classe Exception
    {
    }
}