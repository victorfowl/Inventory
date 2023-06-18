using UnityEngine;

public class ItemSalable : Item, ISalable<float>
{
    /// <summary>
    /// Value of the item, only for items that can be sold.
    /// </summary>
    public float Value;

    public void Sell(float value)
    {
        CharacterManager.inventory.TotalMoney += value;
        CharacterManager.inventory.RemoveFromInventory(this);
    }
}