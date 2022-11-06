using System.Collections.Generic;

public sealed class LetterModel
{
    public void OpenLetter(List<WordLetter> letters, int indexLetter)
    {
        letters[indexLetter].Show();
    }
}
