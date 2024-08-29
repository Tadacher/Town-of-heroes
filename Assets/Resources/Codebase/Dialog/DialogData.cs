using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogs/DialogData", order = 1)]
public class DialogData : ScriptableObject
{
    public DialogEventType EventType;

    public DialogEntry Head;
    public UnityEngine.Object TriggeredType;
    public enum DialogEventType
    {
        Building,
        Mob,
    }
}

