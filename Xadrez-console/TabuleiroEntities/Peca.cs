namespace TabuleiroEntities
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarQuantidadeMovimentos()
        {
            QuantidadeMovimentos++;
        }

        public void DecrementarQuantidadeMovimentos()
        {
            QuantidadeMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matrizMovimentos = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                    if (matrizMovimentos[i, j])
                        return true;
            return false;
        }

        public bool PodeMoverPara(Posicao posicaoDestino)
        {
            return MovimentosPossiveis()[posicaoDestino.Linha, posicaoDestino.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
