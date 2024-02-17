using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    public static LevelState Instance;

    public Action OnLevelReady;
    public Action OnLevelStart;
    public Action OnLevelFinished;
    public Action OnScoreAdded;

    public enum State
    {
        Ready,
        InGame,
        Finished
    }
    public State CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        OnLevelReady?.Invoke();
    }
    public void ChangeState(State state)
    {
        CurrentState = state;
    }
    public void LevelSpawned()
    {
        CurrentState = State.Ready;
        OnLevelReady?.Invoke();
    }
    public void GameStarted()
    {
        CurrentState = State.InGame;
        OnLevelStart?.Invoke();
    }
    public void LevelFinished()
    {
        CurrentState = State.Finished;
        OnLevelFinished?.Invoke();
    }
}