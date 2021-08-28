using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, Dictionary<string, int>> GetBigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var words in text)
            {
                for (int i = 0; i < words.Count - 1; i++)
                {
                    if (!bigrams.ContainsKey(words[i]))
                        bigrams.Add(words[i], new Dictionary<string, int>());
                    if (!bigrams[words[i]].ContainsKey(words[i + 1]))
                        bigrams[words[i]].Add(words[i + 1], 1);
                    else
                        bigrams[words[i]][words[i + 1]] += 1;
                }
            }
            return bigrams;
        }

        public static Dictionary<string, Dictionary<string, int>> GetTrigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> trigrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var words in text)
            {
                for (int i = 0; i < words.Count - 2; i++)
                {
                    if (!trigrams.ContainsKey(words[i] + " " + words[i + 1]))
                        trigrams.Add(words[i] + " " + words[i + 1], new Dictionary<string, int>());
                    if (!trigrams[words[i] + " " + words[i + 1]].ContainsKey(words[i + 2]))
                        trigrams[words[i] + " " + words[i + 1]].Add(words[i + 2], 1);
                    else
                        trigrams[words[i] + " " + words[i + 1]][words[i + 2]] += 1;
                }
            }
            return trigrams;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            Dictionary<string, Dictionary<string, int>> bigrams = GetBigrams(text);
            foreach (var firstWord in bigrams)
            {
                var maxValue = 0;
                string mostFrequentSecondWord = null;
                foreach (var secondWord in firstWord.Value)
                {
                    if (secondWord.Value == maxValue)
                        if (string.CompareOrdinal(mostFrequentSecondWord, secondWord.Key) > 0)
                            mostFrequentSecondWord = secondWord.Key;

                    if (secondWord.Value > maxValue)
                    {
                        mostFrequentSecondWord = secondWord.Key;
                        maxValue = secondWord.Value;
                    }
                }
                result.Add(firstWord.Key, mostFrequentSecondWord);
            }

            Dictionary<string, Dictionary<string, int>> trigrams = GetTrigrams(text);
            foreach (var wordPair in trigrams)
            {
                var maxValue = 0;
                string mostFrequentThirdWord = null;
                foreach (var ThirdWord in wordPair.Value)
                {
                    if (ThirdWord.Value == maxValue)
                        if (string.CompareOrdinal(mostFrequentThirdWord, ThirdWord.Key) > 0)
                            mostFrequentThirdWord = ThirdWord.Key;

                    if (ThirdWord.Value > maxValue)
                    {
                        mostFrequentThirdWord = ThirdWord.Key;
                        maxValue = ThirdWord.Value;
                    }
                }
                result.Add(wordPair.Key, mostFrequentThirdWord);
            }

            return result;
        }
    }
}
