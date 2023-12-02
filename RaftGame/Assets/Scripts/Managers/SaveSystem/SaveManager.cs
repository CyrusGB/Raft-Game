using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour{
    public static SaveManager Instance;
    
    JsonWriter JsonTheScribe;
    string _bldPath = "/Buildings/buidingData.json", _plrPath = "/Players/player0.json";

    #region Data
    WorldData _worldData;
    [SerializeField] PlayerData _playerData;
    #endregion


    void Awake(){
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        JsonTheScribe = new();
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state){
        switch (state){
            case GameManager.GameState.CheckSaves:
                CheckSaves();
                break;
            case GameManager.GameState.CreateSave:
                CreateSave();
                break;
            case GameManager.GameState.LoadSave:
                LoadSave();
                break;
            case GameManager.GameState.SaveGame:
                SaveGame();
                break;
        }
    }

    void CheckSaves(){
        //GameManager.Instance.UpdateGameState(GameManager.GameState.CreateSave);
        if(File.Exists(Application.dataPath + "/SaveData/Buildings/buidingData.json")){
            GameManager.Instance.UpdateGameState(GameManager.GameState.LoadSave);
        }else{
            GameManager.Instance.UpdateGameState(GameManager.GameState.CreateSave);
        }
    }

    void CreateSave(){
        _worldData = new(){buildings = new(), containers = new(), processors = new()};
        AddBuildable2Data(new StaticBuildingData(new Vector2(0,0), "BasicRaft"));
        AddBuildable2Data(new StaticBuildingData(new Vector2(1,0), "BasicRaft"));
        AddBuildable2Data(new StaticBuildingData(new Vector2(2,0), "BasicRaft"));
        List<ItemData> items = new(){new ItemData("WoodenPlank", 1, 0), new ItemData("Rock", 1, 2)};
        InventoryData inventory = new(items);
        StorageData chest = new(new Vector2(1,1), "Chest", inventory);
        AddBuildable2Data(chest);
        _playerData = new(){pos = new Vector2(0, .5f)};
        JsonTheScribe.WriteJson(_playerData, _plrPath);
        JsonTheScribe.WriteJson(_worldData, _bldPath);
        GameManager.Instance.UpdateGameState(GameManager.GameState.LoadSave);
    }

    void AddBuildable2Data(BuildingData bld){
        switch (bld){
            case StaticBuildingData stat:
                _worldData.buildings.Add(stat);
                break;
            case StorageData cont: 
                _worldData.containers.Add(cont);
                break;
            case ItemProcessorData proc:
                _worldData.processors.Add(proc);
                break;
        }
    }

    void LoadSave(){
        _worldData = JsonTheScribe.ReadJson<WorldData>(_bldPath);
        _playerData = JsonTheScribe.ReadJson<PlayerData>(_plrPath);
        GameManager.Instance.UpdateGameState(GameManager.GameState.LoadRaft);
    }

    public void SaveGame(){
        //For player data if there are more we need to account for them here.
        _playerData.pos = PlayerManager.Instance.ReturnPlayer(0).ReturnPos();
        _worldData = GridManager.Instance.CompressPackageIntoWorldData();
        JsonTheScribe.WriteJson(_playerData, "/Players/player0.json");
        JsonTheScribe.WriteJson(_worldData, "/Buildings/buidingData.json");
        Debug.Log("saved");
    }

    public WorldData ReturnWorldData() => _worldData;
    public PlayerData ReturnPlayerData() => _playerData;    
}

public class JsonWriter{
    public void WriteJson(SaveData newData, string path){
        
        string json = JsonUtility.ToJson(newData, true);
        string finalPath = "/SaveData" + path;
        File.WriteAllText(Application.dataPath + finalPath, json);
        Debug.Log("Wrote data at: " + finalPath);
    }

    public T ReadJson<T>(string path) where T : SaveData{
        string json = File.ReadAllText(Application.dataPath + "/SaveData" + path);
        
        var readData = JsonUtility.FromJson<T>(json);
        Debug.Log("Read data at: " + path);
        return readData;
    }
}

//     public static SaveManager Instance;
//     GridData _gridData;
//     List<PlayerData> _playerData = new();
//     JsonWriter jsonTheScribe;
//     string _bldPath = "/Buildings/buidingData.json", _plrPath = "/Players/player0.json";

//     void Awake(){
//         Instance = this;
//         GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
//         jsonTheScribe = new();
//     }

//     void OnDestroy(){
//         GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
//     }

//     private void GameManagerOnGameStateChanged(GameManager.GameState state){
//         switch (state){
//             case GameManager.GameState.CheckSaves:
//                 CheckSaves();
//                 break;
//             case GameManager.GameState.CreateSave:
//                 CreateNewSave();
//                 break;
//             case GameManager.GameState.LoadSave:
//                 LoadSave();
//                 break;
//             case GameManager.GameState.SaveGame:
//                 SaveGame();
//                 break;
//         }
//     }

//     void CheckSaves(){
//         if(File.Exists(Application.dataPath + "/SaveData/Buildings/buidingData.json")){
//             GameManager.Instance.UpdateGameState(GameManager.GameState.LoadSave);
//         }else{
//             GameManager.Instance.UpdateGameState(GameManager.GameState.CreateSave);
//         }
//     }

//     void CreateNewSave(){
//         _gridData = new();
//         AddBuildable2Data(new BuildingData(new Vector2(0,0), "BasicRaft"));
//         AddBuildable2Data(new BuildingData(new Vector2(1,0), "BasicRaft"));
//         AddBuildable2Data(new BuildingData(new Vector2(2,0), "BasicRaft"));
//         //AddBuildable2Data(new BuildingData(new Vector2(1,1), "Chest", new ContainerData(BuildingDictionary.Instance.ReturnBldFromDict("Chest").containerSize)));
//         _playerData.Add(new());
//         _playerData[0].playerPosition = new Vector2(0,.5f);
//         jsonTheScribe.WriteJson(_playerData[0], _plrPath);
//         jsonTheScribe.WriteJson(_gridData, _bldPath);
//         GameManager.Instance.UpdateGameState(GameManager.GameState.LoadSave);
//     }

//     void LoadSave(){
//         if(jsonTheScribe.ReadJson(_bldPath) is GridData grd){_gridData = grd;}
//         if(jsonTheScribe.ReadJson(_plrPath) is PlayerData plr){_playerData.Add(plr);}
//         GameManager.Instance.UpdateGameState(GameManager.GameState.LoadRaft);
//     }

//     public GridData ReturnGridData() => _gridData;

//     public void AddBuildable2Data(BuildingData newBuilding){
//         _gridData.gridList.Add(newBuilding);
//     }

//     public PlayerData ReturnPlayerData(int index) => _playerData[0];

//     public void SaveGame(){
//         //For player data if there are more we need to account for them here.
//         _playerData[0].playerPosition = PlayerManager.Instance.ReturnPlayer(0).ReturnPos();
//         jsonTheScribe.WriteJson(_playerData[0], "/Players/player0.json");
//         jsonTheScribe.WriteJson(_gridData, "/Buildings/buidingData.json");
//         Debug.Log("saved");
//     }
// }