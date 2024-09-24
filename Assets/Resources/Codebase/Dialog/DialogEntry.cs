using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogs/DialogEntry", order = 1)]
public class DialogEntry : ScriptableObject
{
    public string Text;
    public Sprite LeftActor;
    public Sprite RightActor;
    public DialogEntry ChainedEntry;
}