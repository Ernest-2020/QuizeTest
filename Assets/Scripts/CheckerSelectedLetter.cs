using System;

public sealed class CheckerSelectedLetter
{
    public event Action<int> CorrectLetterSelected;
    public event Action UnCorrectLetterSelected;
    public event Action FindedAllLetter;
        
    private int _numberOfCorrectlySelectedLetters;
    public void  Check(string word, string letter)
    {
        string correctLetters = string.Empty;
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i]==letter[0])
            {
                _numberOfCorrectlySelectedLetters++;
                correctLetters += $"{word[i]}";
                CorrectLetterSelected?.Invoke(i);
            }
        }
        if (correctLetters == string.Empty)
        {
            
            UnCorrectLetterSelected?.Invoke();
        }

        if (_numberOfCorrectlySelectedLetters==word.Length)
        {
            FindedAllLetter?.Invoke();
        }
        
    }
}
