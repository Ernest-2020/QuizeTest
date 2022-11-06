using System.Collections.Generic;
using UnityEngine;

public sealed class FinderWords 
{
    private readonly int _minLenghtWord;
    private readonly string _alphabet;
    private readonly string[] _ignorableWords;
    private readonly List<string> _usedWords;
    
    private string _text;

    public FinderWords(TextAsset textAsset,string[] ignorableWords,int minLenghtWord,string alphabet)
    {
        _minLenghtWord = minLenghtWord;
        _ignorableWords = ignorableWords;
        _alphabet = alphabet;
        _text = textAsset.text.ToUpper();
        _usedWords = new List<string>();
    }
    
    public string FindWord(int startIndex)
    {

        var word = string.Empty;
        for (int i = startIndex; i < _text.Length; i++)
        {
            if (!CheckChar(_text[i]))
            {
                for (int j = i; j < _text.Length; j++)
                {
                    if (CheckChar(_text[j]))
                    {
                        word += _text[j];

                    }
                    else if (word != string.Empty&&CheckWord(word) )
                    {
           
                        _usedWords.Add(word);
                        RemoveWord(word, j);
                        return word;
                    }
                    else
                    {
                        word = string.Empty;
                    }
                }
                return string.Empty;
            }
        }
        return string.Empty;
    }

    private bool CheckChar(char checkableChar)
    {
        for (int i = 0; i < _alphabet.Length; i++)
        {
            if (_alphabet[i] == checkableChar)
            {
                return true;
            }
        }
        
        return false;
    }

    private bool CheckLenghtWord(string word)
    {
        if (word.Length >= _minLenghtWord)
        {
            return true;
        }

        return false;
    }

    private void RemoveWord(string word, int indexLastLetter)
    {
        _text = _text.Remove(indexLastLetter - word.Length, word.Length);
    }

    private bool CheckUsedWords(string word)
    {
        if (_usedWords.Count != 0)
        {
            for (int i = 0; i < _usedWords.Count; i++)
            {
                if (word == _usedWords[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CheckIgnorableWords(string word)
    {
        for (int i = 0; i < _ignorableWords.Length; i++)
        {
            if (word == _ignorableWords[i])
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckWord(string word)
    {
        if (CheckUsedWords(word)&&CheckLenghtWord(word)&&CheckIgnorableWords(word))
        {
            return true;
        }
        return false;
    }
}

