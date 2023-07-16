using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWhatFlavors();
        }
        public static void WhatFlavors(List<int> cost, int money)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < cost.Count-1; i++) 
            {
                if (map.ContainsKey(money - cost[i]))
                {
                    int i2 = map[money - cost[i]];
                    Console.WriteLine(i2>i ? $"{i+1} {i2+1}" : $"{i2+1} {i+1}");
                    return;
                }
                else
                {
                    map.Add(cost[i], i);
                }
            }
        }
        public static void WhatFlavors0(List<int> cost, int money)
        {
            for (int i = 0; i < cost.Count - 1; i++)
            {
                int i2 = cost.IndexOf(money - cost[i]);

                if (i2 >= 0 && i2 != i)
                {
                    Console.WriteLine(i2 > i ? $"{i + 1} {i2 + 1}" : $"{i2 + 1} {i + 1}");
                    return;
                }
            }
        }
        public static void TestWhatFlavors()
        {
            WhatFlavors(new List<int>() { 2, 2, 4, 3 }, 4);
        }
        public static void checkMagazine(List<string> magazine, List<string> note)
        {
            // Create a has with words in magazine and quantities
            var dic = new Dictionary<string, int>();
            foreach ( var word in magazine)
            {
                if (dic.ContainsKey(word))
                    dic[word]++;
                else
                    dic.Add(word, 1);
            }

            // Check all word in note
            foreach ( var word in note) 
            {
                if (dic.ContainsKey(word) && dic[word] > 0)
                    dic[word]--;
                else
                {
                    Console.WriteLine("No");
                    return;
                }
            }

            Console.WriteLine("Yes");
            return;

        }
        public static void TestCheckMagazine()
        {
            var magazine = new List<string>() { "Attack", "at", "down" };
            var note = new List<string>(){  "Attack", "at", "down"};
            checkMagazine(magazine, note);
        }
    }
}
