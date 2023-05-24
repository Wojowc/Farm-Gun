using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string headerText;
    public string contentText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(contentText, headerText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    private void OnDisable()
    {
        TooltipSystem.Hide();
    }
}
