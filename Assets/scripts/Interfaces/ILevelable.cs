using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelable
{
    protected delegate void Levelup();
    public abstract void RecieveExperience(int exp);
    
}
