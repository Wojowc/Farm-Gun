using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    GameBaseState currentState;

    public GameStateIntro IntroState = new GameStateIntro();
    public GameStateDiceroll DicerollState = new GameStateDiceroll();
    public GameStateDay DayState = new GameStateDay();
    public GameStateNight NightState = new GameStateNight();
    public GameStateMarket MarketState = new GameStateMarket();
    public GameStateEnd EndState = new GameStateEnd();

    private void Start()
    {
        currentState = IntroState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
