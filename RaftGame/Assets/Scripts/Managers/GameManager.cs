using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    void Awake(){
        Instance = this;
    }

    void Start(){
        UpdateGameState(GameState.CheckSaves);
    }
    
    public void UpdateGameState(GameState newState){
        State = newState;
        switch (newState){
            case GameState.CheckSaves:
                break;
            case GameState.CreateSave:
                break;
            case GameState.LoadSave:
                break;
            case GameState.LoadRaft:
                break;
            case GameState.LoadPlayer:
                break;
            case GameState.GameStart:
                break;
            case GameState.SaveGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState{
        CheckSaves,
        CreateSave,
        LoadSave,
        LoadRaft,
        LoadPlayer,
        GameStart,
        SaveGame
    }
}