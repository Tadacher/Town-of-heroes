using Services.CardGeneration;
using System.Text;
using UnityEngine;
/// <summary>
/// Contains info for world cell card ui, such as description, title, types, etc
/// File name must be same as towerstats file name
/// </summary>
[CreateAssetMenu(fileName = "WorldCellCardInfoConfig", menuName = "ScriptableObjects/WorldCellCardInfoConfig", order = 1)]
public class WorldCellCardInfoConfig : ScriptableObject
{
    public string Header;
    public string Decription;
    public string CellTags => GetCellTags();
    private static StringBuilder _sb = new StringBuilder(30);

    /// <summary>
    /// insert cell to automatically gain its stats to description
    /// </summary>
    public CellStats CellStats;
    private string GetCellTags()
    {
        _sb.Clear();
        foreach (var cellTag in CellStats.CellTags)
        {
            _sb.Append(cellTag.ToString());
            _sb.Append(' ');
        }
        return _sb.ToString();
    }
}
