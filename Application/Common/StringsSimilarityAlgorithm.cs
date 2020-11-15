using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public static class StringsSimilarityAlgorithm
    {
        public static int Calculate(string source, string query)
        {
            source = source.ToLower();
            query = query.ToLower();

            string[] splitSource = source.Split(" ");
            string[] splitQuery = query.Split(" ");

            int scoresSum = 0;

            foreach(string qry in splitQuery)
            {
                foreach(string src in splitSource)
                {
                    int score = LevenshteinDistance.Calculate(qry, src);
                    if(score < (int)Math.Ceiling((float)qry.Length/2))
                    {
                        return 0;
                    }
                    scoresSum += score;
                }
            }

            return scoresSum;
        }
    }
}
