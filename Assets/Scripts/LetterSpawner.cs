using System.Collections.Generic;
using UnityEngine;

public sealed class LetterSpawner
{
    private readonly Pool<AlphabetLetter> _poolAlphabetLetter;
    private readonly Pool<WordLetter> _poolWordLetter;

    private readonly List<AlphabetLetter> _disabledLetterButton;

    public LetterSpawner(AlphabetLetter prefabAlphabetLetter,Transform letterButtonContainer, int countLetterButtons,
                         WordLetter wordLetterPrefab,Transform letterWordContainer,int lenghtLargestWordText)
    {
        _poolAlphabetLetter = new Pool<AlphabetLetter>(letterButtonContainer,countLetterButtons,prefabAlphabetLetter);
        _poolWordLetter = new Pool<WordLetter>(letterWordContainer,lenghtLargestWordText,wordLetterPrefab);
        _disabledLetterButton = new List<AlphabetLetter>();
    }

    public AlphabetLetter SpawnAlphabetLetter()
    {
       AlphabetLetter alphabetShowableText = _poolAlphabetLetter.GetFreeObject();
       return alphabetShowableText;
    }
    
    public WordLetter SpawnLetterWord()
    {
       WordLetter wordLetter =  _poolWordLetter.GetFreeObject();
       return wordLetter;
    }

    public void DisableLetterWord()
    {
        for (int i = 0; i < _poolWordLetter.Objects.Count; i++)
        {
            _poolWordLetter.Objects[i].gameObject.SetActive(false);
        }
    }
    
    public void DisableLetterButton(AlphabetLetter showableTexts)
    {
        showableTexts.gameObject.SetActive(false);
        _disabledLetterButton.Add(showableTexts);
    }

    public void EnableDisabledLetterButton()
    {
        for (int i = 0; i < _disabledLetterButton.Count; i++)
        {
            _disabledLetterButton[i].gameObject.SetActive(true);
        }
        _disabledLetterButton.Clear();
    }

    public void InteractableButton(bool isInteractable = false)
    {
        for (int i = 0; i < _poolAlphabetLetter.Objects.Count; i++)
        {
            _poolAlphabetLetter.Objects[i].Interactable(isInteractable);
        }
    }
}
