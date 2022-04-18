using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            TestActivityNotifications();
        }
        public static int activityNotifications(List<int> expenditure, int d)
        {
            if (expenditure.Count <= d)
                return 0;

            // Create map of frequency - for the first 'd' elements in expenditure
            var map = new int[expenditure.Max()+1];
            for (int i = 0; i < d; i++)
                map[expenditure[i]]++;

            // notifications count
            int notifications = 0;

            // traverse expenditure from d
            for ( int i=d; i< expenditure.Count; i++)
            {
                // Get mediam from map
                decimal median = GetMedianFromMap(map, d);
                if (expenditure[i] >= median * 2)
                    notifications++;

                map[expenditure[i - d]]--;
                map[expenditure[i]]++;
            }

            // return notifications count
            return notifications;

        }
        public static decimal GetMedianFromMap(int[] map, int d) 
        {
            // counter
            int c = 0;

            // Even number of elements
            if (d % 2 == 0)
            {
                foreach (var i in map)
                {
                    c += map[i];
                    if (c == d / 2)
                        if ( i == 1)
                            return ((decimal)map[i] + map[i + 1]) / 2;
                        else
                            return map[i];
                }
            }
            else
            {
                foreach( var i in map)
                {
                    c += map[i];
                    if (c >= d / 2 + 1)
                        return map[i];
                }
            }

            throw new Exception("Error in map");
        }
        public static void TestActivityNotifications()
        {
            var expenditures = new List<int>() { 10, 20, 30, 40, 50 };
            var d = 3;

            Console.WriteLine(activityNotifications(expenditures, d));
            Console.WriteLine("Expected: 1");


            expenditures = new List<int>() { 2, 3, 4, 2, 3, 6, 8, 4, 5 };
            d = 5;

            Console.WriteLine(activityNotifications(expenditures, d));
            Console.WriteLine("Expected: 2");

            expenditures = new List<int>() { 1, 2, 3, 4, 4 };
            d = 4;

            Console.WriteLine(activityNotifications(expenditures, d));
            Console.WriteLine("Expected: 0");

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
