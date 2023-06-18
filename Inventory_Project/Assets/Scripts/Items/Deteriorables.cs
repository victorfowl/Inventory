using System.Collections;
using UnityEngine;

public abstract class Deteriorables : Item
{
    public virtual IEnumerator Deteriorate()
    {
        yield return new WaitForSeconds(1);
        if (Durability > 0)
        {
            Durability--;
            CharacterManager.Instance.StartCoroutine(Deteriorate());
        }
    }
}