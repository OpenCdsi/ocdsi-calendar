﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Duration;

namespace Duration
{
    public class Parser
    {
        private static Regex re = new Regex("^([\\+-]?\\d+)(\\w+)");

        private static Interval ParseUnit(string text)
        {
            text = text.ToLower();
            return (text.First()) switch
            {
                'y' => Interval.Year,
                'm' => Interval.Month,
                'w' => Interval.Week,
                'd' => Interval.Day,
                _ => throw new ArgumentException(text),
            };
        }

        public static IDuration Parse(string text)
        {
            text = text.Replace(" ", "");
            var match = re.Match(text);
            if (match.Success)
            {
                return new Duration()
                {
                    Value = int.Parse(match.Groups[1].Value),
                    Unit = ParseUnit(match.Groups[2].Value)
                };
            }
            else
            {
                throw new ArgumentException(text);
            }
        }

        public static IEnumerable<IDuration> ParseAll(string text)
        {
            var durations = new List<IDuration>();
            text = text.Replace(" ", "");
            while (!string.IsNullOrEmpty(text))
            {
                durations.Add(Parse(text));
                text = re.Replace(text, "");
            }
            return durations;
        }
    }

}