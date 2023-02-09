using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitpoint
{
    delegate void RecieveDamage(int damage);
    public void Heal(int points);

}
