using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Inventory/Resource", order = 1)]
public class Resources : Deteriorables, ISalable<float>
{
    /// <summary>
    /// Value of the item, only for items that can be sold.
    /// </summary>
    public float Value;

    float devaluableStep = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        DeterioredItemEvent.AddListener(() => FindObjectOfType<InspectorItemPanel>(true).SetItemValue(this, Value));
        devaluableStep = Value / Durability;
    }

    public void Sell(float value)
    {
        CharacterManager.inventory.TotalMoney += Mathf.Round(value);
        CharacterManager.inventory.RemoveFromInventory(this);
    }

    public override IEnumerator Deteriorate()
    {
        Value -= devaluableStep;
        return base.Deteriorate();
    }
}
