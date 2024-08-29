using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogs/DialogEntryDatabase", order = 1)]

public class DialogDatabase : ScriptableObject
{
    public DialogData[] _dialogDatas;
}
