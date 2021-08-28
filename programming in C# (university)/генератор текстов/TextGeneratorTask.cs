using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            if (wordsCount == 0 || nextWords.Count == 0) return phraseBeginning;

            if (!phraseBeginning.Contains(" "))
            {
                if (nextWords.ContainsKey(phraseBeginning))
                {
                    wordsCount--;
                    phraseBeginning += " " + nextWords[phraseBeginning];
                }
                else
                    return phraseBeginning;
            }

            for (int i = 0; i < wordsCount; i++)
            {   
                string[] words = phraseBeginning.Split(' ');
    
                if (words.Length >= 2 && nextWords.ContainsKey(words[words.Length - 2] + " " + words[words.Length - 1]))
                    phraseBeginning += " " + nextWords[words[words.Length - 2] + " " + words[words.Length - 1]];
                else if (words.Length >= 1 && nextWords.ContainsKey(words[words.Length - 1]))
                    phraseBeginning += " " + nextWords[words[words.Length - 1]];
            }
            return phraseBeginning;
        }
    }
}
