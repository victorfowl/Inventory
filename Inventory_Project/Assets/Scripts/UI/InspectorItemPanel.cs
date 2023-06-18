using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectorItemPanel : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] TextMeshProUGUI ValueText;
    [SerializeField] TextMeshProUGUI WeightText;
    [SerializeField] TextMeshProUGUI DurabilityText;
    [SerializeField] TextMeshProUGUI DurabilityTitleText;

    Item itemToInspect;

    public void SetItemToInspect(Item item) {
        if (itemToInspect == item)
            return;

        RestartValues();

        itemToInspect = item;
        ItemImage.sprite = item.ItemImage;
        DescriptionText.text = item.Description;
        if(item is ItemSalable)
            ValueText.text = ((ItemSalable)item).Value.ToString();
        else if(item is Resources)
            ValueText.text = Mathf.Round(((Resources)item).Value).ToString();
        WeightText.text = item.ItemWeight.ToString();
        if (item.Durability > 0)
        {
            DurabilityText.gameObject.SetActive(true);
            DurabilityTitleText.gameObject.SetActive(true);
            DurabilityText.text = item.Durability.ToString();
        }
        else
        {
            DurabilityText.gameObject.SetActive(false);
            DurabilityTitleText.gameObject.SetActive(false);
        }
    }

    void RestartValues()
    {
        ItemImage.sprite = null;
        DescriptionText.text = "";
        ValueText.text = "0";
        WeightText.text = "0";
    }

    public void ToogleInspectorPanel(Item inspectedItem)
    {
        if (itemToInspect == inspectedItem)
            gameObject.SetActive(!gameObject.activeSelf);
        else
            gameObject.SetActive(true);
    }
}
