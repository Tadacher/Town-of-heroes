using Core.Towers;
using System;
using System.Collections.Generic;

public class CardAvalilabilityService 
{
    public List<Type> AllowedTypes => _allowedTypes;
    private readonly List<Type> _allowedTypes = new();

    private readonly Type[] _defaulTypes =
    {
        typeof(ArcherTower),
    };

    public CardAvalilabilityService()
    {
        _allowedTypes.AddRange(_defaulTypes);
    }

    public void AddAllowedType(Type type) => _allowedTypes.Add(type);
    public void RemoveAllowedType(Type type)
    {
        if (_allowedTypes.Contains(type))
            _allowedTypes.Remove(type);
    }
}
