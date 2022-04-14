using System;
using System.Collections.Generic;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCheckMagazine();
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
