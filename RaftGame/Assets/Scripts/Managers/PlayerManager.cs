using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] PlayerController playerPrefab;
    List<PlayerController> players = new();

    void Awake(){
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy(){
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    void GameManagerOnGameStateChanged(GameManager.GameState state){
        switch (state){
            case GameManager.GameState.LoadPlayer:
                LoadPlayer(0);
                break;
        }
    }

    void LoadPlayer(int index){
        SaveManager saveManager = SaveManager.Instance;
        PlayerData data = saveManager.ReturnPlayerData();
        PlayerController player = Instantiate(playerPrefab, data.pos, Quaternion.identity);
        players.Add(player);
        GameManager.Instance.UpdateGameState(GameManager.GameState.GameStart);
    }

    public PlayerController ReturnPlayer(int index) => players[0];

    
}
