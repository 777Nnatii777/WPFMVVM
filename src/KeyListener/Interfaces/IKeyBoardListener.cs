
namespace KeyListener.Interfaces
{
    public interface IKeyBoardListener
    {
        void Start();
        void Stop();
        IReadOnlyDictionary<string, int> GetStats();
    }
}