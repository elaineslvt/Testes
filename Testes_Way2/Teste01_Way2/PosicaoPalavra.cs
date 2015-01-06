using System;
using System.Net;

namespace Teste01_Way2
{
    public class PosicaoPalavra
    {
        public int GatinhosMortos { get; set; }
      
        public int PosicaoPalavraDicionario(string pPalavra)
        {
            int posicao = 2000, incremento = 2000, posicaoIni = 2000;

            GatinhosMortos = 0;

            while (true)
            {
                string url = String.Format("http://teste.way2.com.br/dic/api/words/{0}", posicao);

                string retorno = "";
                try
                {
                    GatinhosMortos++;
                    retorno = new WebClient().DownloadString(new Uri(url));
                }
                catch (Exception ex)
                {
                    if (incremento > 1)
                    {
                        // caso a palavra esteja entre os ultimos 2000
                        incremento = incremento / 2;
                        posicao = posicaoIni + incremento;
                        continue;
                    }
                    else
                        return -1;
                }
                
                int resultado = string.Compare(retorno, pPalavra, true);
                switch (resultado)
                {
                    
                    case -1:
                        // -1 retorno menor que pPalavra. Incrementa posicao e posição inicial 
                        posicaoIni = posicao;
                        posicao += incremento;
                        break;

                    case 0:
                        // 0 encontrou a palavra
                        return posicao;
                    case 1:
                        // 1 retorno maior que pPalavra. Diminui o tamanho do incremento
                        incremento = incremento / 2;
                        if (incremento == 0 && posicaoIni <= 2000 && posicao != 0)
                        {
                            // caso a palavra esteja entre os primeiros 2000
                            incremento = posicaoIni / 2;
                            posicaoIni = 0;
                        }
                        else if (incremento == 0) return -1;

                        posicao = posicaoIni + incremento;
                        break;
                }
            }
        }
    }
}

  
