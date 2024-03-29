﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.HelperClasses
{
    
    public class StringMatcher
    {
        public static bool StringsCloselyMatch_IgnoreCase(string first, string second)
        {
            return (DamerauLevenshtein.Distance(first.ToLower().Trim(),
                second.ToLower().Trim()) <= 2);
        }

        public static bool StringsCloselyMatch_IgnoreCaseAndNonAlphaNumeric(string first, string second)
        {
            return StringsCloselyMatch_IgnoreCase(StripNonAlphaNumeric(first), StripNonAlphaNumeric(second));
        }

        private static string StripNonAlphaNumeric(string text)
        {
            return Regex.Replace(text, "[^0-9]", "");
        }
    }

        public class DamerauLevenshtein
        {
            public static int Distance(string source, string target)
            {
                if (String.IsNullOrEmpty(source))
                {
                    if (String.IsNullOrEmpty(target))
                    {
                        return 0;
                    }
                    else
                    {
                        return target.Length;
                    }
                }
                else if (String.IsNullOrEmpty(target))
                {
                    return source.Length;
                }

                var score = new int[source.Length + 2, target.Length + 2];

                var INF = source.Length + target.Length;
                score[0, 0] = INF;
                for (var i = 0; i <= source.Length; i++) { score[i + 1, 1] = i; score[i + 1, 0] = INF; }
                for (var j = 0; j <= target.Length; j++) { score[1, j + 1] = j; score[0, j + 1] = INF; }

                var sd = new SortedDictionary<char, int>();
                foreach (var letter in (source + target))
                {
                    if (!sd.ContainsKey(letter))
                        sd.Add(letter, 0);
                }

                for (var i = 1; i <= source.Length; i++)
                {
                    var DB = 0;
                    for (var j = 1; j <= target.Length; j++)
                    {
                        var i1 = sd[target[j - 1]];
                        var j1 = DB;

                        if (source[i - 1] == target[j - 1])
                        {
                            score[i + 1, j + 1] = score[i, j];
                            DB = j;
                        }
                        else
                        {
                            score[i + 1, j + 1] = Math.Min(score[i, j], Math.Min(score[i + 1, j], score[i, j + 1])) + 1;
                        }

                        score[i + 1, j + 1] = Math.Min(score[i + 1, j + 1], score[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                    }

                    sd[source[i - 1]] = i;
                }

                return score[source.Length + 1, target.Length + 1];
            }
        }

    }
