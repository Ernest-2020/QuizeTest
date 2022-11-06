public sealed class ScoreModel
{
    private int _point;

    public int NumberOf => _point;

    public void Counter(int count)
    {
        _point += count;
    }
}
