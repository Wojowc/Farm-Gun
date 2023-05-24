using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance = null;

    [SerializeField]
    private ModalWindowPanel modalWindow;

    public ModalWindowPanel ModalWindow => modalWindow;

    private void Awake()
    {
        instance = this;
    }
}
