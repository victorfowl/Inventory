using UnityEngine;

public class ScrollSpace : MonoBehaviour
{

    [SerializeField] float compensation = 20;

    RectTransform rectT;

    private void OnEnable()
    {
        rectT = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, (Screen.height / 2) - compensation);
    }
}
