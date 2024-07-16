using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WorldCells
{
    public class WorldCellInfoView : MonoBehaviour
    {
        public Text Title;
        public Text Description;
        public Text Tags;
        private StringBuilder _stringBuilder = new StringBuilder();
        internal void SetTags(CellBiomeTypes[] cellTypes)
        {
            foreach (var cellType in cellTypes)
            {
                _stringBuilder.Append(cellType.ToString());
                _stringBuilder.Append(", ");
                Tags.text = _stringBuilder.ToString();
                _stringBuilder.Clear();
            }
        }
    }
}