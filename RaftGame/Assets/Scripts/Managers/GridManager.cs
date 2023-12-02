using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    void Awake(){
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy(){
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private Dictionary<Vector2, Buildable> _buildables = new();

    private void GameManagerOnGameStateChanged(GameManager.GameState state){
        switch (state){
            case GameManager.GameState.LoadRaft:
                WorldData _worldData = SaveManager.Instance.ReturnWorldData();
                GenerateRaft(_worldData);
                break;
        }
    }

    void GenerateRaft(WorldData newData){
        LoopThrough(newData.buildings);
        LoopThrough(newData.containers);
        LoopThrough(newData.processors);
        GameManager.Instance.UpdateGameState(GameManager.GameState.LoadPlayer);
    }

    void DestroyRaft(){
        foreach(KeyValuePair<Vector2, Buildable> bld in _buildables){
            Destroy(bld.Value.gameObject);
        }
        _buildables = new();
    }

    void LoopThrough<T>(List<T> loop) where T : BuildingData{ //Used for generating Raft
        foreach(T bld in loop){
            // Debug.Log(BuildingDictionary.Instance);
            Buildable newBuildable = Instantiate(BuildingDictionary.Instance.ReturnBldFromDict(bld.bldName).bldPrefab, bld.pos, Quaternion.identity);
            newBuildable.SetData(bld);
            _buildables.Add(bld.pos, newBuildable);
            // Debug.Log(GetTileAtPosition(bld.pos));
        }
    }

    public Buildable GetTileAtPosition(Vector2 pos){
        if(_buildables.TryGetValue(pos,out var tile)){
            return tile;
        }
        return null;
    }

    public void PlaceBuildable(Buildable buildable, Vector2 pos){
        Vector2 roundedPos = Round2Grid(pos);
        if(CheckBldValid(roundedPos, buildable)){
            Buildable newBuildable = Instantiate(buildable, roundedPos, Quaternion.identity);
            _buildables.Add(roundedPos, newBuildable);
            newBuildable.CreateNewData(roundedPos);
        }else{
            Debug.Log("Can't Build There!");
        }
    }

    public void DestroyBuildable(Vector2 pos){
        Vector2 roundedPos = Round2Grid(pos);
        if(GetTileAtPosition(roundedPos) != null){
            Destroy(GetTileAtPosition(roundedPos).gameObject);
            _buildables.Remove(roundedPos);
            Debug.Log("Removed Buildable at: " + pos.ToString());
            return;
        }
        Debug.Log("No Buildable there!");
        
       

    }

    public void ChangeDataAtPos(BuildingData newData, Vector2 pos){
        Vector2 roundedPos = Round2Grid(pos);
        Buildable bld = GetTileAtPosition(roundedPos);
        bld.SetData(newData);
    }

    public WorldData CompressPackageIntoWorldData(){
        WorldData newData = new(){buildings = new(), containers = new(), processors = new()};
        foreach(KeyValuePair<Vector2, Buildable> bld in _buildables){
            BuildingData data = bld.Value.ReturnData();
            switch (data){
            case StaticBuildingData stat:
                newData.buildings.Add(stat);
                break;
            case StorageData cont:
                newData.containers.Add(cont);
                break;
            case ItemProcessorData proc:
                newData.processors.Add(proc);
                break;
            }
        }
        return newData;
    }

    Vector2 Round2Grid(Vector2 pos2Round) => new(Mathf.Round(pos2Round.x), Mathf.Round(pos2Round.y));

    bool CheckBldValid(Vector2 pos, Buildable buildable){
        bool final = true;
        for (int x = 0; x < buildable.ReturnScrObj().bldSize.x; x++){
            for (int y = 0; y < buildable.ReturnScrObj().bldSize.y; y++){
                if(GetTileAtPosition(new Vector2(pos.x + x, pos.y + y)) != null){
                    return false;
                }
            }  
        }

        if(buildable.ReturnScrObj().needsGrounded){
            for (int x = 0; x < buildable.ReturnScrObj().bldSize.x; x++){
                if(GetTileAtPosition(new Vector2(pos.x + x, pos.y -1)) == null){
                    return false;
                }
            }
        }
        return final;
    }

    public Dictionary<Vector2, Buildable> ReturnDictionary() => _buildables;
    public void InteractWith(Vector2 pos){
        Buildable bld = GetTileAtPosition(Round2Grid(pos));
        if(bld !=null && bld is InteractableBuildable interactable){
            interactable.Interact();
        }
    }
}