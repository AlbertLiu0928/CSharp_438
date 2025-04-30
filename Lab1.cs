using System;

class Program
{
    static void Main()
    {
        System.Console.WriteLine("請輸入你的名字");
        string name = Console.ReadLine();


        Random rand = new Random();
        int target = rand.Next(0, 100);
        int min = 0, max = 99;
        int Count = 0;
        bool win = false;

        Console.WriteLine($" {name} 請猜 0~99 之間的整數：");

        while (true)
        {
            Console.Write($"請猜一個數字（目前範圍：{min} ~ {max}）：");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int guess) || guess < min || guess > max)
            {
                Console.WriteLine("請輸入有效的數");
                continue;
            }

            Count++;

            if (Count == 7)
            {
                Console.WriteLine($"猜七次還沒猜到，你該放棄了！正確答案是:{target}");
                break;
            }
            if (guess == target)
            {
                Console.WriteLine($"你贏了！用了 {Count} 次！");
                win = true;
                break;
            }
            else if (max - min == 1)
            {
                Console.WriteLine($"你輸了！正確答案是：{target}（共猜了 {Count} 次）");
                break;
            }
            else if (guess < target)
            {
                Console.WriteLine($"太小了 已猜{Count}次");
                min = guess + 1;
            }
            else
            {
                Console.WriteLine($"太大了 已猜{Count}次");
                max = guess - 1;
            }
        }
    }
}
