using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject ModalWindowBox;

    [Header("Header")]
    [SerializeField]
    private Transform headerArea;
    [SerializeField]
    private TextMeshProUGUI titleField;

    [Header("Content")]
    [SerializeField]
    private Transform contentArea;
    [SerializeField]
    private Transform verticalLayoutArea;
    [SerializeField]
    private Image contentImage;
    [SerializeField]
    private TextMeshProUGUI contentField;

    [Space()]
    [SerializeField]
    private Transform horizontalLayoutArea;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI iconText;

    [Header("Footer")]
    [SerializeField]
    private Transform footerArea;
    [SerializeField]
    private Button confirmButton;
    [SerializeField]
    private Button declineButton;
    [SerializeField]
    private Button alternateButton;

    #endregion

    #region Actions

    private Action onConfirmCallback;
    private Action onDeclineCallback;
    private Action onAlternateCallback;

    #endregion

    #region Methods

    #region BaseButtonMethods

    public void Confirm()
    {
        onConfirmCallback?.Invoke();
        Close();
    }

    public void Decline()
    {
        onDeclineCallback?.Invoke();
        Close();
    }

    public void Alternate()
    {
        onAlternateCallback?.Invoke();
        Close();
    }

    private void Close()
    {
        ModalWindowBox.SetActive(false);
    }

    #endregion 

    #region SettingMethods

    public void ShowAsHero(string title, Sprite imageToShow, string message, Action confirmAction, Action declineAction, Action alternateAction = null)
    {
        // we want the "hero screen"
        horizontalLayoutArea.gameObject.SetActive(false);
        verticalLayoutArea.gameObject.SetActive(false);

        headerArea.gameObject.SetActive(!string.IsNullOrEmpty(title));
        titleField.text = title;

        contentImage.sprite = imageToShow;
        contentField.text = message;

        onConfirmCallback = confirmAction;

        alternateButton.gameObject.SetActive(alternateAction != null);
        onAlternateCallback = alternateAction;

        declineButton.gameObject.SetActive(declineAction != null);
        onDeclineCallback = declineAction;
    }

    #endregion

    #endregion Methods

}
