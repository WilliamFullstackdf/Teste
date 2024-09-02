using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("Escolha a tarefa a ser executada:");
            Console.WriteLine("1 - Calcular a soma dos números naturais até um índice");
            Console.WriteLine("2 - Verificar se um número pertence à sequência de Fibonacci");
            Console.WriteLine("3 - Calcular o faturamento diário");
            Console.WriteLine("4 - Calcular o percentual de faturamento por estado");
            Console.WriteLine("5 - Inverter os caracteres de uma string");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite o número da tarefa (0-5): ");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CalcularSoma();
                        break;
                    case 2:
                        VerificarFibonacci();
                        break;
                    case 3:
                        CalcularFaturamento();
                        break;
                    case 4:
                        CalcularPercentualFaturamento();
                        break;
                    case 5:
                        InverterString();
                        break;
                    case 0:
                        continuar = false;
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, insira um número entre 0 e 5.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, insira um número entre 0 e 5.");
            }
        }
    }

    static void CalcularSoma()
    {
        int INDICE = 13, SOMA = 0, K = 0;
        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }
        Console.WriteLine($"Tarefa 1 - SOMA: {SOMA}");
    }

    static void VerificarFibonacci()
    {
        Console.Write("Digite um número para verificar na sequência de Fibonacci: ");
        if (int.TryParse(Console.ReadLine(), out int num))
        {
            bool isFibonacci = IsFibonacci(num);
            Console.WriteLine(isFibonacci
                ? $"Tarefa 2 - {num} pertence à sequência de Fibonacci."
                : $"Tarefa 2 - {num} não pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine("Entrada inválida. Por favor, insira um número inteiro.");
        }
    }

    static bool IsFibonacci(int number)
    {
        if (number < 0) return false;
        int a = 0, b = 1;
        while (b < number)
        {
            int temp = a;
            a = b;
            b = temp + b;
        }
        return b == number || number == 0;
    }

    static void CalcularFaturamento()
    {
        var jsonFilePath = "faturamento.json";
        Console.WriteLine($"Diretório de execução: {Directory.GetCurrentDirectory()}");

        if (File.Exists(jsonFilePath))
        {
            var json = File.ReadAllText(jsonFilePath);
            var faturamento = JsonConvert.DeserializeObject<decimal[]>(json);

            if (faturamento != null && faturamento.Length > 0)
            {
                var faturamentoDiario = faturamento.Where(f => f > 0).ToArray();
                decimal menorFaturamento = faturamentoDiario.Min();
                decimal maiorFaturamento = faturamentoDiario.Max();
                decimal mediaMensal = faturamentoDiario.Average();
                int diasAcimaMedia = faturamentoDiario.Count(f => f > mediaMensal);

                Console.WriteLine($"Tarefa 3 - Menor Faturamento: {menorFaturamento}");
                Console.WriteLine($"Tarefa 3 - Maior Faturamento: {maiorFaturamento}");
                Console.WriteLine($"Tarefa 3 - Dias acima da média: {diasAcimaMedia}");
            }
            else
            {
                Console.WriteLine("O arquivo JSON está vazio ou não pode ser lido.");
            }
        }
        else
        {
            Console.WriteLine($"Arquivo '{jsonFilePath}' não encontrado.");
        }
    }

    static void CalcularPercentualFaturamento()
    {
        decimal sp = 67836.43m;
        decimal rj = 36678.66m;
        decimal mg = 29229.88m;
        decimal es = 27165.48m;
        decimal outros = 19849.53m;
        decimal total = sp + rj + mg + es + outros;

        decimal percentualSp = (sp / total) * 100;
        decimal percentualRj = (rj / total) * 100;
        decimal percentualMg = (mg / total) * 100;
        decimal percentualEs = (es / total) * 100;
        decimal percentualOutros = (outros / total) * 100;

        Console.WriteLine($"Tarefa 4 - Percentual SP: {percentualSp:F2}%");
        Console.WriteLine($"Tarefa 4 - Percentual RJ: {percentualRj:F2}%");
        Console.WriteLine($"Tarefa 4 - Percentual MG: {percentualMg:F2}%");
        Console.WriteLine($"Tarefa 4 - Percentual ES: {percentualEs:F2}%");
        Console.WriteLine($"Tarefa 4 - Percentual Outros: {percentualOutros:F2}%");
    }

    static void InverterString()
    {
        Console.Write("Digite uma string para inverter: ");
        string input = Console.ReadLine();
        
        if (input != null)
        {
            string reversed = ReverseString(input);
            Console.WriteLine($"Tarefa 5 - String invertida: {reversed}");
        }
        else
        {
            Console.WriteLine("Entrada inválida. A string não pode ser nula.");
        }
    }

    static string ReverseString(string str)
    {
        char[] array = str.ToCharArray();
        int left = 0;
        int right = array.Length - 1;

        while (left < right)
        {
            char temp = array[left];
            array[left] = array[right];
            array[right] = temp;

            left++;
            right--;
        }

        return new string(array);
    }
}
