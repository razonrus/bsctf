using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Tools
{
    public class Tests
    {
        private const string BasePath = @"";

        [Test]
        public void Test()
        {
            var lines = File.ReadAllLines(BasePath + "orig.txt");

            var emails = lines.Select(x => x.Split(',').First().ToLower()).ToList();

            var file = File.ReadAllText(BasePath + "orig.txt").ToLower();

            var foundEmailsCount = emails.Count(x =>
                //new Regex(@"(\W|^)(" + Regex.Escape(x) + @")(\W|$)", RegexOptions.Multiline).IsMatch(file)
                file.Contains(x)
                );

            Console.WriteLine(foundEmailsCount);
            Console.WriteLine(foundEmailsCount/(decimal) emails.Count());
        }

        [Test]
        public void ParseLog()
        {
            var lines = File.ReadAllLines(@"");

            var dictResult = new Dictionary<string, long>();
            var dict = new Dictionary<string, long>();
            var error = 0;
            foreach (var line in lines)
            {
                var splits = line.Split(new[] {"|", " - "}, StringSplitOptions.RemoveEmptyEntries);
                var key = splits.ElementAt(1);
                var value = long.Parse(splits.ElementAt(2));

                if (dict.ContainsKey(key) == false)
                {
                    dict.Add(key, value);
                    dictResult.Add(key, 0);
                }
                else
                {
                    if (value <= dict[key])
                    {
                        if (value == 100)
                            dictResult[key] += dict[key];
                        else
                            error++;
                    }
                    dict[key] = value;
                }
            }
            foreach (var key in dictResult.Keys.ToList())
                dictResult[key] += dict[key];

            foreach (var pair in dictResult.OrderByDescending(x=>x.Value))
            {
                Console.WriteLine(pair.Key +" " + pair.Value);
            }

            Console.WriteLine("Sum: " + dictResult.Sum(x=>x.Value));
            Console.WriteLine("Err: " + error);
        }
    }
}
