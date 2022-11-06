using System;

public sealed class AttemptsModel
    {
        public event Action AttemptsEnded;
        
        private int _numberAttempts;

        public int NumberOf => _numberAttempts;
        
        public AttemptsModel(int numberAttempts)
        {
            _numberAttempts = numberAttempts;
        }
        
        public void Counter()
        {
            _numberAttempts--;
            if (_numberAttempts<=0)
            {
                AttemptsEnded?.Invoke();
            }
        }
    }
