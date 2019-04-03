﻿using System;
using System.IO; // necessário para ler e escrever em arquivos
using static System.Console;
using static Utilitarios;

namespace TPProjetoI
{
	class Program
	{
		private static void SeletorDeOpcoes()
		{
			int opcao;

			do
			{
				Clear();
				WritePos(10, 0, "TP - Projeto I");

				WritePos(2, 2, "1 – Estatística de uma lista de valores lidos de um arquivo texto");
				WritePos(2, 3, "2 – MMC entre dois valores digitados");
				WritePos(2, 4, "3 – Raiz Cúbica de um valor digitado");
				WritePos(2, 5, "4 – MDC por subtrações sucessivas");
				WritePos(2, 6, "5 – Lista de números de Fibonacci");
				WritePos(2, 7, "6 – Seno Hiperbólico");

				WritePos(2, 20, "99 – Terminar o programa");

				WritePos(2, 22, "Opção: ");
				opcao = int.Parse(ReadLine());

				switch (opcao)
				{
					case 1: LerArquivo(); break;
					case 2: break;
					case 3: AproximacaoDaRaizCubica(); break;
					case 4: MDCporSubtracoes(); break;
					case 5: ListarFibonacci(); break;
					case 6: break;
				}
			}
			while (opcao != 99);
		}

		private static void LerArquivo()
		{
			Clear();
			WritePos(2, 2, "Insira o nome do arquivo texto: ");
			WritePos(2, 3, "(localizado na pasta root do projeto)");
			WritePos(2, 4, @"..\..\                          .txt");
			SetCursorPosition(8, 4);
			var caminho = @"..\..\file.txt";
			try // executar o código verificando por exceções
			{
				var arquivo = ReadLine();
				if (arquivo.EndsWith(".txt"))									// verifica se o usuário inseriu ".txt" no fim do 
				{																// nome do arquivo, se não, insere automaticamente
					caminho = @"..\..\" + arquivo;
				}
				else															// também é feita uma concatenação, completando o nome
				{																// do arquivo com um comando para voltar duas pastas no
					caminho = @"..\..\" + arquivo + ".txt";						// sistema de arquivos, de modo a encontrar o arquivo na
				}																// pasta root do projeto, e não em "\bin\Debug"

				var reader = new StreamReader(caminho);							// instancia um StreamReader usando a string concatenada com o arquivo
				var somaGeral = new Somatoria();								// somatória de todos os números lidos
				var somaV = new Somatoria();									// somatória dos valores V lidos com peso P
				var somaP = new Somatoria();									// soamtória apenas dos valores P lidos
				var prodGeral = new Produtorio();								// produtório de todos os números lidos
				var somaInversos = new Somatoria();								// somatória dos inversos de todos os números lidos
				while (!reader.EndOfStream)
				{
					string linhaLida = reader.ReadLine();						// lê a linha e divide os valores em
					double v = double.Parse(linhaLida.Substring(0, 8));			// v,
					double p = double.Parse(linhaLida.Substring(8, 8));			// p

					somaGeral.Somar(v);											// adiciona v e p à soma geral
					somaGeral.Somar(p);
					somaV.Somar(v * p);											// adiciona v com peso p à somaV
					somaP.Somar(p);												// adiciona p à somaP
					prodGeral.Multiplicar(v);									// adiciona v e p ao produtório geral
					prodGeral.Multiplicar(p);
					somaInversos.Somar(1 / v);									// adiciona os inversos de v e p à soma dos inversos
					somaInversos.Somar(1 / p);
				}																// repete até o fim do arquivo
				var mat = new MatematicaDouble(prodGeral.Valor);
				WritePos(2, 6, $"RMQ = {Math.Sqrt(somaGeral.MediaAritmetica())}");
				WritePos(2, 7, $"MA = {somaGeral.MediaAritmetica()}");
				WritePos(2, 8, $"MP = {somaV.Valor / somaP.Valor}");
				WritePos(2, 9, $"MG = {mat.EnesimaRaiz(prodGeral.Qtos)}");
				WritePos(2, 10, $"MH = {somaGeral.Valor / somaInversos.Valor}");
			}
			catch (Exception e) // receber a exceção e escrever sua mensagem
			{
				WritePos(2, 5, "O arquivo não pode ser lido:\n\n");
				WriteLine(e.Message);
			}
			EsperarEnter();
		}

		private static void CalcularMMC()
		{
			Clear();
			WritePos(2, 1, "Insira o primeiro valor: ");
			int a = int.Parse(ReadLine());
			WritePos(2, 2, "Insira o segundo valor: ");
			int b = int.Parse(ReadLine());
			var mat = new Matematica(a);

		}

		private static void MDCporSubtracoes()
		{
			Clear();
			WritePos(5, 1, "Calculo de MDC por subtrações sucessivas");
			WritePos(2, 3, "Insira o primeiro valor: ");
			int a = int.Parse(ReadLine());
			WritePos(2, 4, "Insira o segundo valor: ");
			int b = int.Parse(ReadLine());

			var mat = new Matematica(a);

			WritePos(2, 6, $"O MDC de {a} e {b} é {mat.MDCPorSubtracoes(b)}");  // calculando MDC entre "a" e "b"
			EsperarEnter();
		}

		private static void AproximacaoDaRaizCubica()
		{
			Clear();
			WritePos(5, 1, "Aproximação de raíz cúbica");
			WritePos(2, 3, "Digite o valor a ser calculado: ");
			int valor = int.Parse(ReadLine());                                  // recebendo o valor a ser calculado
			WritePos(2, 4, "Digite a margem de erro entre 0,001 e 0,06: ");
			double margem = double.Parse(ReadLine());                           // recebendo a margem de erro
			if (margem >= 0.06 || margem < 0.001)                               // verifica se a margem corresponde a um valor entre 0.001 e 0.06
			{
				WritePos(2, 6, "Valor da margem inválido.");
			}
			else
			{
				var mat = new Matematica(valor);
				WritePos(2, 6, $"O valor aproximado da raíz cúbica de {valor} é {mat.AproximacaoRaizCubica(margem)}");
				WritePos(2, 7, $"Arredondando: {Math.Round(mat.AproximacaoRaizCubica(margem))}");
			}
			EsperarEnter();
		}

		private static void ListarFibonacci()
		{
			Clear();
			WritePos(2, 2, "Insira a quantidade desejada de números de Fibonacci: ");
			int n = int.Parse(ReadLine());
			WriteLine();
			var mat = new Matematica(n);
			foreach (double a in mat.Fibonacci())
			{
				WriteLine(a);
			}
			EsperarEnter();
		}

		static void Main(string[] args)
		{
			SeletorDeOpcoes();
		}
	}
}
