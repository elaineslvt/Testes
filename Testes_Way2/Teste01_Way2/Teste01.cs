using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste01_Way2
{
    class Teste01
    {
        [STAThread]
        static void Main(string[] args)
        {
            PosicaoPalavra posicaoPalavra = new PosicaoPalavra();

            try
            {
                Console.WriteLine("Digite a palavra:");

                string palavra = Console.ReadLine();
                int posicao = posicaoPalavra.PosicaoPalavraDicionario(palavra);

                if (posicao == -1)
                    Console.WriteLine(string.Format("Palavra {0} não encontrada no dicionário", palavra));
                else
                    Console.WriteLine(string.Format("Palavra {0} encontrada na posição {1}", palavra, posicao));

                Console.WriteLine(string.Format("Gatinhos mortos: {0}", posicaoPalavra.GatinhosMortos));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


