using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public abstract class Deteriorables : Item
{

    public UnityEvent DeterioredItemEvent = new UnityEvent();

    private void OnEnable()
    {
        DeterioredItemEvent.AddListener(() => FindObjectOfType<InspectorItemPanel>(true).SetItemDurability(this, Durability));
    }

    public virtual IEnumerator Deteriorate()
    {
        yield return new WaitForSeconds(1);
        if (Durability > 0)
        {
            Durability--;
            DeterioredItemEvent.Invoke();
            CharacterManager.Instance.StartCoroutine(Deteriorate());
        }
    }
}