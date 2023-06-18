using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ItemPanel : Image, IPointerDownHandler
{

    RectTransform m_RectTransform;
    Button equipButton;
    Button sellButton;
    Button deleteButton;

    Item item;


    protected override void OnEnable()
    {
        base.OnEnable();
        m_RectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        m_RectTransform.position = Input.mousePosition;
    }
    protected override void Awake()
    {
        equipButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        sellButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        deleteButton = transform.GetChild(0).GetChild(2).GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_RectTransform.rect.Contains(eventData.position))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetItem(Item itemToSet) {
        if (item != itemToSet)
        {
            item = itemToSet;
            SetButtons();
        }

    }

    void SetButtons()
    {
        equipButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();
        if (item is IEquipable)
        {
            equipButton.interactable = true;
            equipButton.onClick.AddListener(() => ((IEquipable)item).Equip());
        }
        else if (item is IUsable)
        {
            equipButton.interactable = true;
            equipButton.onClick.AddListener(() => ((IUsable)item).Use());
        }
        else
            equipButton.interactable = false;

        if (item is ISalable<float>)
        {
            sellButton.interactable = true;
            if(item is ItemSalable)
                sellButton.onClick.AddListener(() => ((ISalable<float>)item).Sell(((ItemSalable)item).Value));
            else
                sellButton.onClick.AddListener(() => ((ISalable<float>)item).Sell(((Resources)item).Value));
        }
        else
            sellButton.interactable = false;

        deleteButton.onClick.AddListener(() => CharacterManager.inventory.RemoveFromInventory(item));
    }
}
