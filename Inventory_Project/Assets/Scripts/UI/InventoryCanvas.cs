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
        actualItems = CharacterManager.Instance.inventory.GetItems();
        CharacterManager.Instance.inventory.ItemAdded.AddListener(() => SetItemsList(actualFilter));
        CharacterManager.Instance.inventory.ItemAdded.AddListener(UpdateMoneyAndWeight);

        CharacterManager.Instance.inventory.ItemRemoved.AddListener(_ => GenerateItemButtons(actualItems));
        CharacterManager.Instance.inventory.ItemRemoved.AddListener(_ => UpdateMoneyAndWeight());
        CharacterManager.Instance.inventory.ItemRemoved.AddListener(RemoveFromCanvasInventory);

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
                actualItems.AddRange(CharacterManager.Instance.inventory.GetItems());
                break;
            case ButtonFilters.Filters.Weapons:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(ActiveEquipment), Equipment.TypeOfEquipable.Weapon);
                break;
            case ButtonFilters.Filters.Armour:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Armour);
                actualItems.AddRange(CharacterManager.Instance.inventory.FilterItems(typeof(ActiveEquipment), Equipment.TypeOfEquipable.Shield));
                break;
            case ButtonFilters.Filters.Accesories:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Ring);
                actualItems.AddRange(CharacterManager.Instance.inventory.FilterItems(typeof(Equipment), Equipment.TypeOfEquipable.Necklace));
                break;
            case ButtonFilters.Filters.Consumables:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(Consumables));
                actualItems.AddRange(CharacterManager.Instance.inventory.FilterItems(typeof(Potions)));
                break;
            case ButtonFilters.Filters.Resources:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(Resources));
                break;
            case ButtonFilters.Filters.Trash:
                actualItems = CharacterManager.Instance.inventory.FilterItems(typeof(Item));
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
            GameObject child = itemScroll.GetChild(i).gameObject;
            ButtonItems button = child.GetComponent<ButtonItems>();

            if ((i - 1) < itemsToShow.Count)
            {
                float auxIndex = itemsToShow.Count;
                child.SetActive(true);
                child.GetComponentInChildren<TextMeshProUGUI>(true).text = itemsToShow[i - 1].name;
                auxIndex -= i;
                button.onClick.AddListener(() => SetScrollBarValue(stepSize * auxIndex));
                button.relatedItem = itemsToShow[i - 1];
            }
            else
            {
                child.gameObject.SetActive(false);
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
        moneyText.text = CharacterManager.Instance.inventory.TotalMoney.ToString();
        weightText.text = CharacterManager.Instance.inventory.actualWeight.ToString() + "/" + CharacterManager.Instance.inventory.MaxWeight.ToString();
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