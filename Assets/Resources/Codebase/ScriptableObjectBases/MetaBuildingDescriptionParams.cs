using Progress;
using UnityEngine;

namespace Metagameplay.Ui
{
    [CreateAssetMenu(fileName = "BuildingDescruptionParams", menuName = "ScriptableObjects/BuildingDescruptionParams", order = 1)]
    public class MetaBuildingDescriptionParams : ScriptableObject
    {
        public string Name;
        public string Description;
        public ResourceData Cost;
        public Sprite Image;
        public AudioClip ClipOnSelect;    
    }
}