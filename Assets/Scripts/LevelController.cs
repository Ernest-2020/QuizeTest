using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class LevelController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private TextAsset textAsset;

   
    
    [Header("Prefabs")]
    [SerializeField] private AlphabetLetter alphabetLetterPrefab;
    [SerializeField] private WordLetter wordLetterPrefab;
    
    [Header("Containers")]
    [SerializeField] private Transform containerLettersAlphabet;
    [SerializeField] private Transform containerLettersWord;

    [Header("Hud")] 
    [SerializeField] private AttemptsView attemptsView;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private Messages messages;

    [Header("Message texts")] 
    [SerializeField] private string winText;
    [SerializeField] private string loseText;
    [SerializeField] private string gamePassedText;
    
    [SerializeField] private float messageDisplayTime;
    
    private FinderWords _finderWords;
    private LetterSpawner _letterSpawner;
    private CheckerSelectedLetter _checkerSelectedLetter;
    private LetterModel _letterModel;
    private AttemptsModel _attemptsModel;
    private ScoreModel _scoreModel;
    
    private List<WordLetter> _lettersWord;
    private List<AlphabetLetter> _alphabetLetters;

    private string _foundWord;
    
    private void Awake()
    {
        _letterModel = new LetterModel();
        _scoreModel = new ScoreModel();
        _lettersWord = new List<WordLetter>();
        _alphabetLetters = new List<AlphabetLetter>();
        
        _letterSpawner = new LetterSpawner(alphabetLetterPrefab,containerLettersAlphabet,levelSettings.Alphabet.Length,
                                           wordLetterPrefab,containerLettersWord,levelSettings.LenghtLargesWordText);
        UpdateChecker();
        UpdateFinder();
        SubscribesChecker();
    }

    private void Start()
    {
        StartGame();
    }

    private void OnDisable()
    {
        UnSubscribesChecker();
        UnsubscribesAlphabetLetter();
    }

    private void StartGame()
    {
        UpdateAttempts();
        UpdateFinder();
        
        _foundWord = GetWord();
        
        CreateAlphabetLetter();
        CreateLettersWord();
        
    }
    
    private void NewRound()
    {
        StartCoroutine(NewRoundCoroutine());
    }
    
    private void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }
    
    private void EndAttempts()
    {
        StartCoroutine(EndAttemptsCoroutine());
    }

    private IEnumerator NewRoundCoroutine()
    {
        _letterSpawner.InteractableButton();
        ShowMessage(winText);
        
        yield return new WaitForSeconds(messageDisplayTime);
        
        UpdateDataRound();
        if (_foundWord == string.Empty)
        {
            yield return null;
        }
        _letterSpawner.InteractableButton(true);
    }
    
    private IEnumerator RestartCoroutine()
    {
        _letterSpawner.InteractableButton();
        ShowMessage(gamePassedText);
        
        yield return new WaitForSeconds(messageDisplayTime);
        
        UpdateFinder();
        UpdateDataRound();
        UpdateScore();
        _letterSpawner.InteractableButton(true);
    }
    
    private IEnumerator EndAttemptsCoroutine()
    {
        _letterSpawner.InteractableButton();
        ShowMessage(loseText);
        
        yield return new WaitForSeconds(messageDisplayTime);
        
        UpdateDataRound();
        if (_foundWord == string.Empty)
        {
            yield return null;
        }
        UpdateScore();
        _letterSpawner.InteractableButton(true);
    }

    private void UpdateDataRound()
    {
        messages.Hide();
        
        _letterSpawner.EnableDisabledLetterButton();
        _scoreModel.Counter(_attemptsModel.NumberOf);
        
        scoreView.UpdateText($"{_scoreModel.NumberOf}");
        
        UpdateAttempts();
        UpdateChecker();
        
        _foundWord = GetWord();
        _letterSpawner.DisableLetterWord();
        _lettersWord.Clear();
        
        CreateLettersWord();
    }

    private void UpdateScore()
    {
        _scoreModel = new ScoreModel();
        scoreView.UpdateText($"{_scoreModel.NumberOf}");
    }

    private void UpdateFinder()
    {
        _finderWords = new FinderWords(textAsset,levelSettings.IgnorableWords, levelSettings.MinLenghtWord, levelSettings.Alphabet);
    }

    private void UpdateChecker()
    {
        if (_checkerSelectedLetter != null)
        {
            UnSubscribesChecker();
        }
        _checkerSelectedLetter = new CheckerSelectedLetter();
        SubscribesChecker();
    }
    
    private void UpdateAttempts()
    {
        if (_attemptsModel!=null)
        {
            _attemptsModel.AttemptsEnded -= UpdateAttempts;
            _attemptsModel.AttemptsEnded -= EndAttempts;
        }
        _attemptsModel = new AttemptsModel(levelSettings.NumberAttempts);
        attemptsView.UpdateText($"{levelSettings.NumberAttempts}");
        _attemptsModel.AttemptsEnded += UpdateAttempts;
        _attemptsModel.AttemptsEnded += EndAttempts;
    }
    
    private void ShowMessage(string message)
    {
        messages.UpdateText(message);
        messages.Show();
    }

    private void CreateAlphabetLetter()
    {
        for (int i = 0; i < levelSettings.Alphabet.Length; i++)
        {
            string letter = $"{levelSettings.Alphabet[i]}";
            AlphabetLetter alphabetLetter = _letterSpawner.SpawnAlphabetLetter();
            alphabetLetter.UpdateText(letter);
            alphabetLetter.ClickedButton += CheckWord;
            alphabetLetter.DisablingLetter += _letterSpawner.DisableLetterButton;
            _alphabetLetters.Add(alphabetLetter);
        }
    }

    private void CreateLettersWord()
    {
        for (int i = 0; i < _foundWord.Length; i++)
        {
            string letter = $"{_foundWord[i]}";
            WordLetter wordLetter = _letterSpawner.SpawnLetterWord();
            wordLetter.UpdateText(letter);
            _lettersWord.Add(wordLetter);
        }
    }

    private void Attempt()
    {
        _attemptsModel.Counter();
        attemptsView.UpdateText($"{_attemptsModel.NumberOf}");
    }
    
    private string GetWord()
    {
        int startIndex = Random.Range(0, textAsset.text.Length);
        string word = _finderWords.FindWord(startIndex);
        if (word==string.Empty)
        {
             startIndex = 0;
             word = _finderWords.FindWord(startIndex);
             if (word==string.Empty)
             {
                 StopAllCoroutines();
                 Restart();
                 return string.Empty;
             }
        }
        return word;
    }

    private void ShowCorrectLetterSelected(int indexCorrectSelectLetter)
    {
        _letterModel.OpenLetter(_lettersWord,indexCorrectSelectLetter);
    }
    
    private void CheckWord(string letter)
    {
        _checkerSelectedLetter.Check(_foundWord,letter );
    }

    private void SubscribesChecker()
    {
        _checkerSelectedLetter.CorrectLetterSelected += ShowCorrectLetterSelected;
        _checkerSelectedLetter.UnCorrectLetterSelected += Attempt;
        _checkerSelectedLetter.FindedAllLetter += NewRound;
    }

    private void UnSubscribesChecker()
    {
        _checkerSelectedLetter.CorrectLetterSelected -= ShowCorrectLetterSelected;
        _checkerSelectedLetter.UnCorrectLetterSelected -= Attempt;
        _checkerSelectedLetter.FindedAllLetter -= NewRound;
    }

    private void UnsubscribesAlphabetLetter()
    {
        for (int i = 0; i < _alphabetLetters.Count; i++)
        {
            _alphabetLetters[i].ClickedButton -= CheckWord;
            _alphabetLetters[i].DisablingLetter -= _letterSpawner.DisableLetterButton;
        }
    }
}