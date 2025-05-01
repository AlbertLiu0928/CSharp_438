using System;
using System.Collections.Generic;

class VirusChain
{
    static void Main()
    {
        int N = 16;
        int[] contact = GenerateRandomContacts(N);

        // 顯示完整接觸列表（排版方式照順序）
        Console.WriteLine("接觸紀錄如下：");
        Console.Write("ID:         ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{i,3} ");
        }
        Console.WriteLine();

        Console.Write("Contactee:  ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{contact[i],3} ");
        }
        Console.WriteLine();

        // 顯示感染路徑
        Console.WriteLine("\nInfection Chain starting from ID 0:");
        List<int> chain = GetInfectionChain(contact, 0);
        Console.WriteLine(string.Join(" -> ", chain));
    }

    // 產生隨機打亂的接觸對象
    static int[] GenerateRandomContacts(int N)
    {
        List<int> ids = new List<int>();
        for (int i = 0; i < N; i++)
            ids.Add(i);

        // Fisher-Yates 洗牌
        Random rand = new Random();
        for (int i = N - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            int temp = ids[i];
            ids[i] = ids[j];
            ids[j] = temp;
        }

        return ids.ToArray();
    }

    // 從起始 ID 追蹤感染路徑
    static List<int> GetInfectionChain(int[] contact, int startID)
    {
        List<int> chain = new List<int>();
        HashSet<int> visited = new HashSet<int>();

        int current = startID;
        while (!visited.Contains(current))
        {
            chain.Add(current);
            visited.Add(current);
            current = contact[current];
        }

        return chain;
    }
}
