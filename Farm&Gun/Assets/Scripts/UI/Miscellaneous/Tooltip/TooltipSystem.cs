using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public Tooltip tooltip;

    private void Awake()
    {
       // instance = this;
    }

    public static void Show(string content, string header = "")
    {
      //  instance.tooltip.gameObject.SetActive(true);
        //instance.tooltip.SetText(content, header);
    }

    public static void Hide()
    {
       // instance.tooltip.gameObject.SetActive(false);
    }
}
