using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateIntro : GameBaseState
{
    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private Button confirmButton;

    private bool skipIntroPanel = false;

    public override void EnterState(GameStateManager game)
    {
        Time.timeScale = 0;
        Debug.Log("Entered intro state.");
        skipIntroPanel = false;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => { introPanel.SetActive(false); });
    }

    public override void UpdateState(GameStateManager game)
    {
        if(skipIntroPanel)
        {
            introPanel.SetActive(false);
            game.SwitchState(game.DicerollState);
        }
    }
}
