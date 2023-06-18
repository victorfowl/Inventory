using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    /// <summary>
    /// The total ammount of money you have.
    /// </summary>
    public float TotalMoney = 0;

    /// <summary>
    /// List of items readonly.
    /// </summary>
    public readonly List<Item> itemsList = new List<Item>();

    /// <summary>
    /// The maximum amount of weight that this inventory can store expressed in kilograms.
    /// </summary>
    public float MaxWeight = 100;

    /// <summary>
    /// The actual amount of weight that this inventory can store expressed in kilograms.
    /// </summary>
    public float actualWeight = 0;

    /// <summary>
    /// Event called when you add an item.
    /// </summary>
    public UnityEvent ItemAdded = new UnityEvent();

    /// <summary>
    /// Event called when you remove an item.
    /// </summary>
    public UnityEvent<Item> ItemRemoved = new UnityEvent<Item>();

    /// <summary>
    /// For usual filtering.
    /// </summary>
    /// <param name="type"></param>
    public List<Item> FilterItems(Type type)
    {
        List<Item> filteredItems = new List<Item>();    
        foreach (Item item in itemsList)
        {
            if (item.GetType() == type)
                filteredItems.Add(item);
        }
        return filteredItems;
    }
    /// <summary>
    /// Call this only if the type is Equipment
    /// </summary>
    /// <param name="type"></param>
    /// <param name="typeOfEquip"></param>
    public List<Item> FilterItems(Type type, Equipment.TypeOfEquipable typeOfEquip)
    {
        List<Item> filteredItems = new List<Item>();
        if (type == typeof(Equipment) || type == typeof(ActiveEquipment))
        {
            filteredItems.Clear();
            foreach (Item item in itemsList)
            {
                if (item.GetType() == type && ((Equipment)item).typeOfEquipable == typeOfEquip)
                    filteredItems.Add(item);
            }
        }
        return filteredItems;
    }

    /// <summary>
    /// Add item to inventory method.
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    public bool AddToInventory(Item itemToAdd) {
        Item CopiedItem = CopyItem(itemToAdd);
        CopiedItem.MakeTrash.AddListener(ReplaceItemWithTrash);
        if (actualWeight + CopiedItem.ItemWeight <= MaxWeight)
        {
            itemsList.Add(CopiedItem);
            actualWeight += CopiedItem.ItemWeight;
            ItemAdded.Invoke();
            if (CopiedItem is Deteriorables)
                CharacterManager.Instance.StartCoroutine(((Deteriorables)CopiedItem).Deteriorate());
            return true;
        }
        else return false;
    }

    //Function to make a copy of the object we want to put in the inventory.
    Item CopyItem(Item itemToCopy)
    {
        var itemCopied = UnityEngine.Object.Instantiate(itemToCopy);
        itemCopied.name = itemToCopy.name;
        itemCopied.Description = itemToCopy.Description;
        itemCopied.ItemWeight = itemToCopy.ItemWeight;
        itemCopied.ItemImage = itemToCopy.ItemImage;
        itemCopied.Durability = itemToCopy.Durability;
        if (itemCopied is ItemSalable)
            ((ItemSalable)itemCopied).Value = ((ItemSalable)itemToCopy).Value;
        if(itemCopied is Equipment)
            ((Equipment)itemCopied).typeOfEquipable = ((Equipment)itemToCopy).typeOfEquipable;
        if (itemCopied is ActiveEquipment)
        {
            ((ActiveEquipment)itemCopied).PrefabMesh = ((ActiveEquipment)itemToCopy).PrefabMesh;
            ((ActiveEquipment)itemCopied).Ammunition = ((ActiveEquipment)itemToCopy).Ammunition;
        }
        return itemCopied;
    }

    /// <summary>
    /// Remove from inventory method.
    /// </summary>
    /// <param name="itemToRemove"></param>
    /// <returns></returns>
    public bool RemoveFromInventory(Item itemToRemove) {
        if (itemsList.Contains(itemToRemove))
        {
            itemsList.Remove(itemToRemove);
            actualWeight -= itemToRemove.ItemWeight;
            ItemRemoved.Invoke(itemToRemove);
            return true;
        }else return false;
    }

    /// <summary>
    /// Retrieves only one item from the inventory.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Item GetItem(int index)
    {
        if(index < itemsList.Count)
            return itemsList[index];
        else return null;
    }

    /// <summary>
    /// Retrieve all items in the inventory.
    /// </summary>
    /// <returns></returns>
    public List<Item> GetItems()
    {
        return itemsList;
    }

    //Method to replace the item that has no durability for trash.
    void ReplaceItemWithTrash(Item item) {
        Item itemTrashed = ScriptableObject.CreateInstance<Item>();
        itemTrashed.name = item.name + "(Trash)";
        itemTrashed.ItemWeight = item.ItemWeight;      
        RemoveFromInventory(item);
        AddToInventory(itemTrashed);
    }

}
