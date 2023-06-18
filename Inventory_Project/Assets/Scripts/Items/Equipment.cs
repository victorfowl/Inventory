using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment", order = 1)]
public class Equipment : ItemSalable, IEquipable
{

    public enum TypeOfEquipable { Weapon, Shield, Armour, Necklace, Ring};

    /// <summary>
    /// Type of equipable, if you choose weapon or shield I recommend you to use the class ActiveEquipment instead.
    /// </summary>
    public TypeOfEquipable typeOfEquipable;

    public void Equip()
    {
        CharacterManager.Instance.EquipItem(this);
        Debug.Log("You equiped " + name);
    }
}
