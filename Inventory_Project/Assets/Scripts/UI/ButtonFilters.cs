using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFilters : Button
{
    TextMeshProUGUI text;

    public enum Filters { None, Weapons, Armour, Accesories, Consumables, Resources, Trash};
    [SerializeField] Filters filters = Filters.None;    

    protected override void OnEnable()
    {
        base.OnEnable();
        text = GetComponentInChildren<TextMeshProUGUI>();        
    }

    protected override void Awake()
    {
        base.Awake();
        InventoryCanvas inventoryCanvas = FindObjectOfType<InventoryCanvas>();
        onClick.AddListener(() => inventoryCanvas.ShowHideItemsMenu(this));
        onClick.AddListener(() => inventoryCanvas.SetItemsList(filters));
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!(EventSystem.current.currentSelectedGameObject == gameObject))
            text.color = colors.highlightedColor;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!(EventSystem.current.currentSelectedGameObject == this.gameObject))
            text.color = colors.normalColor;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        text.color = colors.normalColor;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        text.color = colors.selectedColor;
    }

}
