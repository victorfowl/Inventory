using UnityEngine.EventSystems;

public class ButtonItems : ButtonFilters
{

    public Item relatedItem;

    ItemPanel ItemPanel;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemPanel = FindObjectOfType<ItemPanel>(true);
    }

    protected override void Awake()
    {
        InspectorItemPanel panelItem = FindObjectOfType<InspectorItemPanel>(true);
        onClick.AddListener(() => panelItem.ToogleInspectorPanel(relatedItem));
        onClick.AddListener(() => panelItem.SetItemToInspect(relatedItem));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.button.Equals(PointerEventData.InputButton.Right))
        {
            ItemPanel.gameObject.SetActive(true);
            ItemPanel.SetItem(relatedItem);
        }
    }
}
