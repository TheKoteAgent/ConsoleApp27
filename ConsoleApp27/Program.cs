using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = Enumerable.Range(1, 100).ToList();
        List<int> primes = numbers.Where(IsPrime).ToList();
        List<int> fibonacciNumbers = GenerateFibonacci(100);

        File.WriteAllLines("primes.txt", primes.Select(p => p.ToString()));
        File.WriteAllLines("fibonacci.txt", fibonacciNumbers.Select(f => f.ToString()));

        Console.WriteLine("Прості числа збережено в primes.txt");
        Console.WriteLine("Числа Фібоначчі збережено в fibonacci.txt");

        Console.WriteLine("Введіть шлях до файлу:");
        string filename = Console.ReadLine();
        Console.WriteLine("Введіть слово для пошуку:");
        string searchWord = Console.ReadLine();
        Console.WriteLine("Введіть слово для заміни:");
        string replaceWord = Console.ReadLine();

        SearchAndReplace(filename, searchWord, replaceWord);
        Console.WriteLine("Заміна виконана успішно.");

        Console.WriteLine("Введіть шлях до файлу з текстом:");
        string inputFilename = Console.ReadLine();
        Console.WriteLine("Введіть шлях до файлу зі словами для модерації:");
        string moderationFilename = Console.ReadLine();
        Console.WriteLine("Введіть шлях до вихідного файлу:");
        string outputFilename = Console.ReadLine();

        ModerateText(inputFilename, moderationFilename, outputFilename);
        Console.WriteLine("Модерація завершена.");

        Console.WriteLine("Введіть шлях до вихідного файлу:");
        string inputFilenameReverse = Console.ReadLine();
        Console.WriteLine("Введіть шлях до нового файлу:");
        string outputFilenameReverse = Console.ReadLine();

        ReverseFile(inputFilenameReverse, outputFilenameReverse);
        Console.WriteLine("Файл успішно перевернуто.");

        Console.WriteLine("Введіть шлях до файлу з числами:");
        string numbersFilename = Console.ReadLine();
        AnalyzeNumbers(numbersFilename);
    }

    static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;
        for (int i = 5; i * i <= n; i += 6)
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        return true;
    }

    static List<int> GenerateFibonacci(int n)
    {
        List<int> fibonacciSequence = new List<int> { 0, 1 };
        while (fibonacciSequence.Count < n)
        {
            int nextFib = fibonacciSequence[^1] + fibonacciSequence[^2];
            fibonacciSequence.Add(nextFib);
        }
        return fibonacciSequence;
    }

    static void SearchAndReplace(string filename, string searchWord, string replaceWord)
    {
        string content = File.ReadAllText(filename);
        content = content.Replace(searchWord, replaceWord);
        File.WriteAllText(filename, content);
    }

    static void ModerateText(string inputFilename, string moderationFilename, string outputFilename)
    {
        string text = File.ReadAllText(inputFilename);
        string[] moderatedWords = File.ReadAllLines(moderationFilename);

        foreach (string word in moderatedWords)
        {
            text = text.Replace(word, new string('*', word.Length));
        }

        File.WriteAllText(outputFilename, text);
    }

    static void ReverseFile(string inputFilename, string outputFilename)
    {
        string content = File.ReadAllText(inputFilename);
        char[] charArray = content.ToCharArray();
        Array.Reverse(charArray);
        File.WriteAllText(outputFilename, new string(charArray));
    }

    static void AnalyzeNumbers(string filename)
    {
        int positiveCount = 0;
        int negativeCount = 0;
        int twoDigitCount = 0;
        int fiveDigitCount = 0;

        string[] numbers = File.ReadAllLines(filename);

        foreach (string number in numbers)
        {
            int num = int.Parse(number);
            if (num > 0) positiveCount++;
            else if (num < 0) negativeCount++;
            if (number.Length == 2) twoDigitCount++;
            else if (number.Length == 5) fiveDigitCount++;
        }

        Console.WriteLine("Кількість додатних чисел: " + positiveCount);
        Console.WriteLine("Кількість від'ємних чисел: " + negativeCount);
        Console.WriteLine("Кількість двозначних чисел: " + twoDigitCount);
        Console.WriteLine("Кількість п'ятизначних чисел: " + fiveDigitCount);

        File.WriteAllLines("positive_numbers.txt", numbers.Where(x => int.Parse(x) > 0));
        File.WriteAllLines("negative_numbers.txt", numbers.Where(x => int.Parse(x) < 0));
    }
}
