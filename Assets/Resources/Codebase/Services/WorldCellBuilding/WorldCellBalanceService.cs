using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldCells
{
    public class WorldCellBalanceService
    {
        public Dictionary<CellBiomeTypes, int> CellCountByTag { get; private set; }
        public WorldCellBalanceService() =>
            CellCountByTag = new Dictionary<CellBiomeTypes, int>(1);

        public void Count(IEnumerable<CellBiomeTypes> cellTags)
        {
            foreach (CellBiomeTypes tag in cellTags)
            {
                if (CellCountByTag.ContainsKey(tag))
                    CellCountByTag[tag]++;
                else
                    CellCountByTag.Add(tag, 1);
                Debug.Log($"{tag} count {CellCountByTag[tag]}");
            }
        }
        public void UnCount(IEnumerable<CellBiomeTypes> cellTags)
        {
            foreach (CellBiomeTypes tag in cellTags)
            {
                if (CellCountByTag.ContainsKey(tag))
                    CellCountByTag[tag]--;
                else
                    CellCountByTag.Add(tag, 1);
                Debug.Log($"{tag} UnCount {CellCountByTag[tag]}");
            }
        }
        public void Clear(IEnumerable<CellBiomeTypes> tags)
        {
            foreach (CellBiomeTypes tag in tags)
            {
                if (CellCountByTag.ContainsKey(tag))
                    CellCountByTag[tag]--;

                else
                    CellCountByTag.Add(tag, 1);
            }
        } 
    }
}