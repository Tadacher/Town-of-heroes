using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogEntryDatabase", order = 1)]

public class DialogDatabase : ScriptableObject, IInitializableConfig
{
    public Dictionary<string, DialogEntry> DialogDict => _dialogDict;

    [SerializeField] private DialogEntry[] _dialogHeads;
    private Dictionary<string, DialogEntry> _dialogDict;

    private bool _inited = false;
    public void Initialize()
    {
        if (_dialogDict != null)
            return;
        _dialogDict = new();
        foreach (var head in _dialogHeads)
        {
            _dialogDict.Add(head.name, head);
        }
    }
}
