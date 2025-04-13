using System.Collections.Generic;
using KeyListener.Interfaces;

namespace KeyListener.Services
{

public class KeyboardListenerService : IKeyBoardListener
{
 
 private readonly Dictionary<string, int> _keyStats = new();

public void Start()
{
 
 _keyStats.Clear();
 _keyStats["A"] = 3;
 _keyStats["B"] = 5;
 _keyStats["C"] = 7;
 _keyStats["Enter"] = 2;

}

public void Stop()
{


}


public IReadOnlyDictionary<string, int> GetStats()
{
    return _keyStats;

}

}
}