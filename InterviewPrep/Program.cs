using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPairs();
        }
        public static int Pairs(int k, List<int> arr)
        {
            var l = new List<int>();
            int c = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                if (l.Contains(arr[i] - k))
                    c++;
                if (l.Contains(arr[i] + k))
                    c++;
                l.Add(arr[i]);
            }

            return c;
        }
        public static void TestPairs()
        {
            Console.WriteLine(Pairs(2, new List<int>() { 1, 5, 3, 4, 2 }));
        }
        public static string gridChallenge(List<string> grid)
        {
            char[][] grid2 = new char[grid.Count()][];
            
            for (int row = 0; row < grid.Count; row++)
            {
                grid2[row] = grid[row].ToArray();
                Array.Sort(grid2[row]);
            }

            for ( int col=0; col < grid2[0].Count(); col++)
            {
                for ( int row=0; row < grid2.Count()-1; row++)
                {
                    if (grid2[row][col] > grid2[row+1][col])
                        return "NO";
                }
            }

            return "YES";
        }
        public static void TestGridChallenge()
        {

            var list = new List<string>() { "ebacd", "fghij", "olmkn", "trpqs", "xywuv" };
            Console.WriteLine(gridChallenge(list));

        }
        public static int getWinnerTotalGoals(string competition, int year)
        {
            var winner = GetWinner(competition, year);
            var goals = GetTotalGoalsInCompetition(competition, winner, year);

            return goals;
        }
        public static int GetTotalGoalsInCompetition(string competition, string team, int year)
        {
            var lGames = GetGoals(team, year);
            var g = 0;

            foreach (var item in lGames.Where(p=>p.competition==competition))
            {
                if (item.team1 == team)
                    g += Int32.Parse(item.team1goals);
                else
                    g += Int32.Parse(item.team2goals);
            }

            return g;
        }
        public static int GetTotalGoals(string team, int year)
        {
            var lGames = GetGoals(team, year);
            var g = 0;

            foreach (var item in lGames)
            {
                if (item.team1.ToLower() == team.ToLower()) 
                    g += Int32.Parse(item.team1goals);
                else
                    g += Int32.Parse(item.team2goals);
            }

            return g;
        }
        public static void TestGetGoals()
        {
            var g = GetTotalGoals("Barcelona", 2011);
            Console.WriteLine(g);
        }
        public static List<Games> GetGoals(string team, int year)
        {
            int page = 1;
            var lGames = new List<Games>();

            // HTTP client
            HttpClient httpClient = new();

            bool hasData;
            do
            {
                var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";

                // GET request
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = httpClient.Send(request);
                response.EnsureSuccessStatusCode();

                using var streamReader = new StreamReader(response.Content.ReadAsStream());

                // Deserialize and return response
                var root = JsonConvert.DeserializeObject<Root>(streamReader.ReadToEnd());

                foreach (var game in root.data)
                {
                    lGames.Add(game);
                }

                hasData = root.data != null && root.data.Count > 0;
                page++;

            } while (hasData);


            page = 1;
            do
            {
                var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";

                // GET request
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = httpClient.Send(request);
                response.EnsureSuccessStatusCode();

                using var streamReader = new StreamReader(response.Content.ReadAsStream());

                // Deserialize and return response
                var root = JsonConvert.DeserializeObject<Root>(streamReader.ReadToEnd());

                foreach (var game in root.data)
                {
                    lGames.Add(game);
                }

                hasData = root.data != null && root.data.Count > 0;
                page++;

            } while (hasData);


            httpClient.Dispose();

            return lGames;

        }
        public static string GetWinner(string competition, int year)
        {
            // HTTP client
            HttpClient httpClient = new();

            var url = $"https://jsonmock.hackerrank.com/api/football_competitions?name={competition}&year={year}";

            // GET request
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = httpClient.Send(request);
            response.EnsureSuccessStatusCode();

            using var streamReader = new StreamReader(response.Content.ReadAsStream());

            // Deserialize and return response
            var root = JsonConvert.DeserializeObject<Root2>(streamReader.ReadToEnd());


            httpClient.Dispose();

            return root.data[0].winner;

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
