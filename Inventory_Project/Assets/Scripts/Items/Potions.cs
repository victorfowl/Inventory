using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Inventory/Potion", order = 1)]
public class Potions : Consumables
{
    /// <summary>
    /// Ammount of healing or mana to restore.
    /// </summary>
    public float Ammount;

    public enum TypeOfPotion { Heal, Mana}

    /// <summary>
    /// Type of potion you want to create.
    /// </summary>
    public TypeOfPotion typeOfPotion;

    public override void Use()
    {
        switch (typeOfPotion)
        {
            case TypeOfPotion.Heal:
                CharacterManager.Instance.Heal(Ammount);
                break;
            case TypeOfPotion.Mana:
                CharacterManager.Instance.ManaRecover(Ammount);
                break;
            default:
                break;
        }
        base.Use();
    }
}
