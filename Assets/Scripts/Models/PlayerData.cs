using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlayerData
    {
        public int level;
        public long xp;
        public int coins;
        public List<string> unlockedItems;
    }
}