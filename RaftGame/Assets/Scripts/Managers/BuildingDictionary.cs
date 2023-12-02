using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingDictionary : MonoBehaviour
{
    public static BuildingDictionary Instance;
    Dictionary<string, BuildableObj> _buildingDictionary = new();

    void Awake(){
        BuildingDictionary.Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        List<BuildableObj> _newList = Resources.LoadAll<BuildableObj>("Buildables").ToList();
        foreach(BuildableObj bld in _newList){
            _buildingDictionary.Add(bld.bldName, bld);
        }
    }

    void OnDestroy(){
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state){
        switch (state){
            case GameManager.GameState.LoadSave:
                GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
                break;
        }
    }

    public BuildableObj ReturnBldFromDict(string id){
        if(_buildingDictionary.TryGetValue(id,out var bld)){
            return bld;
        }
        return null;
    }



}
