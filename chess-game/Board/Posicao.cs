namespace chess_game;

class Posicao
{
    public int Linha { get; set; }
    public int Coluna { get; set; }
    public Posicao()
    {
    }
    public Posicao(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }
    override public string ToString()
    {
        return Linha + ", " + Coluna;
    }
}
