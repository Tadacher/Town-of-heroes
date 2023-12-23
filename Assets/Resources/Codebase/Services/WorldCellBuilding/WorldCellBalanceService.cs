using System.Collections.Generic;
using UnityEngine;

namespace WorldCells
{
    public class WorldCellBalanceService
    {
        public Dictionary<CellTags, int> CellCountByTag { get; private set; }
        public WorldCellBalanceService() =>
            CellCountByTag = new Dictionary<CellTags, int>(1);

        public void Count(IEnumerable<CellTags> tags)
        {
            foreach (CellTags tag in tags)
            {
                if (CellCountByTag.ContainsKey(tag))
                    CellCountByTag[tag]++;
                else
                    CellCountByTag.Add(tag, 1);
                Debug.Log($"{tag} count {CellCountByTag[tag]}");
            }
        }
        public void Clear(IEnumerable<CellTags> tags)
        {
            foreach (CellTags tag in tags)
            {
                if (CellCountByTag.ContainsKey(tag))
                    CellCountByTag[tag]--;

                else
                    CellCountByTag.Add(tag, 1);
            }
        }
    }
}