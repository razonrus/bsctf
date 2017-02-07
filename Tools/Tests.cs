using System;
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
            Console.WriteLine(foundEmailsCount / (decimal) emails.Count());
        }
    }
}
