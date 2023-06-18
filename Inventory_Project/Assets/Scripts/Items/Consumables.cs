using UnityEngine;
using UnityEngine.Events;

public abstract class Consumables : Deteriorables, IUsable
{
    public virtual void Use()
    {
        Durability = 0;
    }
}
