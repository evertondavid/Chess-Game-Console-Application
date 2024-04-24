namespace chess_game;
public class Piece
{
    public Position? Position { get; set; }
    public Color Color { get; protected set;}
    public int MoveCount { get; protected set; }
    public Board? Board { get; protected set; }

    public Piece (Position position, Board board, Color color)
    {
        Position = position;
        Board = board;
        Color = color;
        MoveCount = 0;
    }
}
