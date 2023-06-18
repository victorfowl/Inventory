using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    [SerializeField] Transform itemScroll;
    [SerializeField] Scrollbar itemScrollBar;
    [SerializeField] GameObject itemMenu;
    [SerializeField] InspectorItemPanel itemInspector;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI weightText;

    ButtonFilters actualButtonFilter;
    ButtonFilters.Filters actualFilter;
    List<Item> actualItems;

    private void OnEnable()
    {
        actualItems = CharacterManager.inventory.GetItems();
        CharacterManager.inventory.ItemAdded.AddListener(() => SetItemsList(actualFilter));
        CharacterManager.inventory.ItemAdded.AddListener(UpdateMoneyAndWeight);

        CharacterManager.inventory.ItemRemoved.AddListener(_ => GenerateItemButtons(actualItems));
        CharacterManager.inventory.ItemRemoved.AddListener(_ => UpdateMoneyAndWeight());
        CharacterManager.inventory.ItemRemoved.AddListener(RemoveFromCanvasInventory);

        UpdateMoneyAndWeight();
    }

    public void ShowHideItemsMenu(ButtonFilters filter)
    {
        if(actualButtonFilter == filter)
            itemMenu.SetActive(!itemMenu.activeSelf);
        else itemMenu.SetActive(true);
        actualButtonFilter = filter;
    }

    public void SetItemsList(ButtonFilters.Filters filter)
    {

        actualItems = new List<Item>();
        switch (filter)
        {
            case ButtonFilters.Filters.None:
                actualItems.AddRange(CharacterManager.inventory.GetItems());
                break;
            case ButtonFilters.Filters.Weapons:
                actualItems = CharacterManager.inventory.FilterItems(typeof(ActiveEquipment), Equipment.TypeOfEquipable.Weapon);
                break;
            case ButtonFilters.Filters.Armour:
                actualItems = CharacterManager.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Armour);
                actualItems.AddRange(CharacterManager.inventory.FilterItems(typeof(ActiveEquipment), Equipment.TypeOfEquipable.Shield));
                break;
            case ButtonFilters.Filters.Accesories:
                actualItems = CharacterManager.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Ring);
                actualItems.AddRange(CharacterManager.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Necklace));
                break;
            case ButtonFilters.Filters.Consumables:
                actualItems = CharacterManager.inventory.FilterItems(typeof(Consumables));
                actualItems.AddRange(CharacterManager.inventory.FilterItems(typeof(Potions)));
                break;
            case ButtonFilters.Filters.Resources:
                actualItems = CharacterManager.inventory.FilterItems(typeof(Resources));
                break;
            case ButtonFilters.Filters.Trash:
                actualItems = CharacterManager.inventory.FilterItems(typeof(Item));
                break;
            default:
                break;
        }
        actualItems.Sort(new SortItemByName());
        GenerateItemButtons(actualItems);
        actualFilter = filter;
    }

    public void GenerateItemButtons(List<Item> itemsToShow)
    {
        float stepSize = 1f / (itemsToShow.Count);

        for (int i = 1; i < itemScroll.childCount - 1; i++)
        {
            if ((i - 1) < itemsToShow.Count)
            {
                float auxIndex = itemsToShow.Count;
                itemScroll.GetChild(i).gameObject.SetActive(true);
                itemScroll.GetChild(i).GetComponentInChildren<TextMeshProUGUI>(true).text = itemsToShow[i - 1].name;
                auxIndex -= i;
                itemScroll.GetChild(i).GetComponent<ButtonItems>().onClick.AddListener(() => SetScrollBarValue(stepSize * auxIndex));
                itemScroll.GetChild(i).GetComponent<ButtonItems>().relatedItem = itemsToShow[i - 1];
            }
            else
            {
                itemScroll.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void RemoveFromCanvasInventory(Item itemToRemove)
    {
        if (itemInspector.gameObject.activeSelf)
            itemInspector.ToogleInspectorPanel(itemToRemove);
        actualItems.Remove(itemToRemove);
        GenerateItemButtons(actualItems);
    }

    void UpdateMoneyAndWeight() {
        moneyText.text = CharacterManager.inventory.TotalMoney.ToString();
        weightText.text = CharacterManager.inventory.actualWeight.ToString() + "/" + CharacterManager.inventory.MaxWeight.ToString();
    }

    void SetScrollBarValue(float value) {
        itemScrollBar.value = value;
    }

    public void CloseApplication() { 
        Application.Quit();
    }
}
public class SortItemByName : IComparer<Item>
{
    public int Compare(Item x, Item y)
    {
        return x.name.CompareTo(y.name);
    }
}