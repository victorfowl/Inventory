using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    [SerializeField] Transform rightHand, leftHand;

    private static CharacterManager instance;

    static GameObject go;

    private CharacterManager(){}

    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance =  go.AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    public static Inventory inventory;
    ActiveEquipment[] Weapons = new ActiveEquipment[2];
    Equipment[] Rings = new Equipment[2];
    Equipment Necklace;
    Equipment Armour;

    private void OnEnable()
    {
        go = gameObject;
        if (gameObject.GetComponent<CharacterManager>()) {
            instance = this;
        }
    }

    private void Awake()
    {
        inventory = new Inventory();
    }

    public void PickUpItem(Item item) {
        inventory.AddToInventory(item);
    }

    public bool EquipItem(Equipment item)
    {
        switch (item.typeOfEquipable)
        {
            case Equipment.TypeOfEquipable.Weapon:
                for (int i = 0; i < Weapons.Length; i++) {
                    if (!Weapons[i])
                    {
                        Weapons[i] = (ActiveEquipment)item;
                        UpdateMeshes();
                        return true;
                    }
                }
                Weapons[0] = (ActiveEquipment)item;
                UpdateMeshes();
                return true;
            case Equipment.TypeOfEquipable.Shield:
                for (int i = 0; i < Weapons.Length; i++)
                {
                    if (Weapons[i]?.typeOfEquipable == Equipment.TypeOfEquipable.Shield)
                    {
                        Weapons[i] = (ActiveEquipment)item;
                        UpdateMeshes();
                        return true;
                    }
                }
                for (int i = 0; i < Weapons.Length; i++)
                {
                    if (!Weapons[i])
                    {
                        Weapons[i] = (ActiveEquipment)item;
                        UpdateMeshes();
                        return true;
                    }
                }
                Weapons[1] = (ActiveEquipment)item;
                UpdateMeshes();
                return true;
            case Equipment.TypeOfEquipable.Armour:
                Armour = item;
                return true;
            case Equipment.TypeOfEquipable.Necklace:
                Necklace = item;
                return true;
            case Equipment.TypeOfEquipable.Ring:
                for (int i = 0; i < Rings.Length; i++)
                {
                    if (!Rings[i])
                    {
                        Rings[i] = item;
                        return true;
                    }
                }
                Rings[0] = item;
                return true;
            default:
                break;
        }
        return false;
    }

    void UpdateMeshes()
    {
        if (rightHand.childCount > 0)
            Destroy(rightHand.GetChild(0).gameObject);
        if (leftHand.childCount > 0)
            Destroy(leftHand.GetChild(0).gameObject);

        if (Weapons[0] && Weapons[0] is ActiveEquipment)
        {
            Instantiate(Weapons[0].PrefabMesh, rightHand);
        }
        if (Weapons[1] && Weapons[1] is ActiveEquipment)
        {
            Instantiate(Weapons[1].PrefabMesh, leftHand);
        }
    }

    public void UseRight()
    {
        if (Weapons[0] != null)
        {
            if (!Weapons[0].Ammunition)
            {
                ((IUsable)Weapons[0]).Use();
                if (Weapons[0].typeOfEquipable == Equipment.TypeOfEquipable.Weapon)
                {
                    GetComponent<Animator>().SetTrigger("AttackRight");
                }
                else if (Weapons[0].typeOfEquipable == Equipment.TypeOfEquipable.Shield)
                {
                    GetComponent<Animator>().SetTrigger("BlockRight");
                }

                if (Weapons[0].Durability <= 0)
                {
                    Destroy(rightHand.GetChild(0).gameObject);
                    Weapons[0] = null;
                }
            }
            else
            {
                foreach (Item resource in inventory.GetItems())
                {
                    if (resource.name == Weapons[0].Ammunition.name)
                    {
                        ((IUsable)Weapons[0]).Use();
                        if (Weapons[0].typeOfEquipable == Equipment.TypeOfEquipable.Weapon)
                        {
                            GetComponent<Animator>().SetTrigger("AttackRight");
                        }
                        else if (Weapons[0].typeOfEquipable == Equipment.TypeOfEquipable.Shield)
                        {
                            GetComponent<Animator>().SetTrigger("BlockRight");
                        }

                        if (Weapons[0].Durability <= 0)
                        {
                            Destroy(rightHand.GetChild(0).gameObject);
                            Weapons[0] = null;
                        }
                        inventory.RemoveFromInventory(resource);
                        break;
                    }
                }
            }
        }
    }
    public void UseLeft()
    {
        if (Weapons[1] != null)
        {
            if (!Weapons[1].Ammunition)
            {
                ((IUsable)Weapons[1]).Use();
                if (Weapons[1].typeOfEquipable == Equipment.TypeOfEquipable.Weapon)
                {
                    GetComponent<Animator>().SetTrigger("AttackLeft");
                }
                else if (Weapons[1].typeOfEquipable == Equipment.TypeOfEquipable.Shield)
                {
                    GetComponent<Animator>().SetTrigger("BlockLeft");
                }

                if (Weapons[1].Durability <= 0)
                {
                    Destroy(leftHand.GetChild(0).gameObject);
                    Weapons[1] = null;
                }
            } else
            {
                foreach (Item resource in inventory.GetItems())
                {
                    if (resource.name == Weapons[1].Ammunition.name)
                    {
                        ((IUsable)Weapons[1]).Use();
                        if (Weapons[1].typeOfEquipable == Equipment.TypeOfEquipable.Weapon)
                        {
                            GetComponent<Animator>().SetTrigger("AttackLeft");
                        }
                        else if (Weapons[1].typeOfEquipable == Equipment.TypeOfEquipable.Shield)
                        {
                            GetComponent<Animator>().SetTrigger("BlockLeft");
                        }

                        if (Weapons[1].Durability <= 0)
                        {
                            Destroy(leftHand.GetChild(0).gameObject);
                            Weapons[1] = null;
                        }
                        inventory.RemoveFromInventory(resource);
                    }

                }
            }
        }
    }

    public void Heal(float healingAmmount) {
        Debug.Log("You've been healed " + healingAmmount + " life points");
    }
    public void ManaRecover(float manaAmmount)
    {
        Debug.Log("You've been restored " + manaAmmount + " mana points");
    }
}
