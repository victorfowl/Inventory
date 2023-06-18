using UnityEngine;

[CreateAssetMenu(fileName = "ActiveEquipment", menuName = "Inventory/ActiveEquipment", order = 1)]
public class ActiveEquipment : Equipment, IUsable
{
    /// <summary>
    /// Prefab of the weapon or shield you want to show when you equip it.
    /// </summary>
    public GameObject PrefabMesh;

    /// <summary>
    /// Ammunition it uses, if it's not set the Equipment will not use any ammunition.
    /// </summary>
    public Resources Ammunition;

    public void Use()
    {
        if (!Ammunition)
            Durability -= 10;
        else
        {
            foreach (Item resource in CharacterManager.inventory.GetItems())
            {
                if (resource.name == Ammunition.name)
                {
                    Durability -= 10;
                }
            }
        }
    }
}