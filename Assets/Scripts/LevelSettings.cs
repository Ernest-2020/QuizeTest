
    using UnityEngine;
    [CreateAssetMenu(fileName = "NewSettings",menuName = "Game/"+nameof(LevelSettings),order = 0)]
    public sealed class LevelSettings:ScriptableObject
    {
        [SerializeField] private int minLenghtWord;
        [SerializeField] private int numberAttempts;
        [SerializeField] private string alphabet;
        [SerializeField] private int lenghtLargesWordText;
        [SerializeField] private string[] ignorableWords;

        public string[] IgnorableWords => ignorableWords;

        public int MinLenghtWord => minLenghtWord;

        public int NumberAttempts => numberAttempts;

        public string Alphabet => alphabet;

        public int LenghtLargesWordText => lenghtLargesWordText;
    }
