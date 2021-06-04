namespace MundoFashion.Domain
{
    public struct Imagem
    {
        public string ImagemLocalizacao { get; private set; }
        public string ImagemUrl { get; private set; }
        public string NomeImagem { get; private set; }

        public Imagem(string nomeImagem, string imagemLocalizacao)
        {
            ImagemLocalizacao = imagemLocalizacao;
            ImagemUrl = @$"http://projeto-mundofashion-bucket.storage.googleapis.com/{nomeImagem}";
            NomeImagem = nomeImagem;
        }
    }
}
