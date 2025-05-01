using System;

class Program
{
    static Random rand = new Random();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("請選擇模式：");
            Console.WriteLine("1. 玩家手動猜數字");
            Console.WriteLine("2. 電腦模擬猜數字");
            Console.Write("請輸入 1 或 2：");

            string modeInput = Console.ReadLine();
            if (modeInput == "1")
            {
                ManualGuess();
                break;
            }
            else if (modeInput == "2")
            {
                RunSimulation();
                break;
            }
            else
            {
                Console.WriteLine("無效輸入，請重新輸入。");
            }
        }
    }

    static void ManualGuess()
    {
        Console.WriteLine("請輸入你的名字：");
        string name = Console.ReadLine();

        int target = rand.Next(0, 100);
        int min = 0, max = 99;
        int count = 0;

        Console.WriteLine($"{name} 請猜 0~99 之間的整數：");

        while (true)
        {
            Console.Write($"請猜一個數字（目前範圍：{min} ~ {max}）：");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int guess) || guess < min || guess > max)
            {
                Console.WriteLine("請輸入有效的數字。");
                continue;
            }

            count++;

            if (guess == target)
            {
                Console.WriteLine($"你贏了！用了 {count} 次！");
                break;
            }
            else if (count == 7)
            {
                Console.WriteLine($"猜七次還沒猜到，遊戲結束。答案是：{target}");
                break;
            }
            else if (max - min == 1 && guess != target)
            {
                Console.WriteLine("2選1還猜錯，你輸了！");
                break;
            }
            else if (guess < target)
            {
                Console.WriteLine($"太小了（第 {count} 次），還有 {7 - count} 次機會");
                min = guess + 1;
            }
            else
            {
                Console.WriteLine($"太大了（第 {count} 次），還有 {7 - count} 次機會");
                max = guess - 1;
            }
        }
    }

    static void RunSimulation()
    {
        string[] methods = { "1", "2", "3", "4" };
        string method;
        int simulations;

        while (true)
        {
            Console.WriteLine("請選擇策略（輸入數字）：1.random / 2.binary / 3.linear / 4.ternary");
            method = Console.ReadLine();

            if (Array.Exists(methods, m => m == method))
                break;

            Console.WriteLine("策略無效，請重新輸入。");
        }

        while (true)
        {
            Console.Write("請輸入模擬次數（正整數）：");
            string input = Console.ReadLine();

            if (int.TryParse(input, out simulations) && simulations > 0)
                break;

            Console.WriteLine("請輸入有效的模擬次數。");
        }

        int success = 0;
        for (int i = 0; i < simulations; i++)
        {
            if (SimulateGuess(method))
                success++;
        }

        string strategyName = method switch
        {
            "1" => "random",
            "2" => "binary",
            "3" => "linear",
            "4" => "ternary",
            _ => "unknown"
        };

        Console.WriteLine($"\n模擬策略：{strategyName}");
        Console.WriteLine($"模擬完成，共 {simulations} 次");
        Console.WriteLine($"成功次數：{success} 次");
        Console.WriteLine($"勝率：{(double)success / simulations * 100:F2}%");
    }

    static bool SimulateGuess(string method)
    {
        int target = rand.Next(0, 100);
        int min = 0, max = 99;
        int count = 0;

        while (count < 7)
        {
            int guess = 0;

            switch (method)
            {
                case "1":
                    guess = rand.Next(min, max + 1);
                    break;
                case "2":
                    guess = (min + max) / 2;
                    break;
                case "3":
                    guess = min;
                    break;
                case "4":
                    guess = min + (max - min) * 2 / 3;
                    break;
            }

            count++;

            if (guess == target)
                return true;

            if (max - min == 1 && guess != target)
                return false;

            if (guess < target)
                min = guess + 1;
            else
                max = guess - 1;

            if (min > max)
                break;
        }

        return false;
    }
}
