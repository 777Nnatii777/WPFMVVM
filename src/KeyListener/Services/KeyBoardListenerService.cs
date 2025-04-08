using System.Collections.Generic;
using KeyListener.Interfaces;

namespace KeyListener.Services
{
    public class KeyboardListenerService : IKeyBoardListener
    {
        private Dictionary<string, int> _keyStats = new();

        public void Start() 
        {

        }


        public void Stop() 
        { 

        }

        public IReadOnlyDictionary<string, int> GetStats() => _keyStats;
    }
}
