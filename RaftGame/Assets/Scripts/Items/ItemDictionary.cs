using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDictionary : MonoBehaviour
{
    public static ItemDictionary Instance;
    Dictionary<string, ItemObj> _itemDictionary = new();

    void Awake(){
        ItemDictionary.Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        List<ItemObj> _newList = Resources.LoadAll<ItemObj>("Items").ToList();
        foreach(ItemObj item in _newList){
            _itemDictionary.Add(item.itemName, item);
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

    public ItemObj ReturnItemFromDict(string id){
        if(_itemDictionary.TryGetValue(id,out var item)){
            return item;
        }
        return ReturnItemFromDict("PlaceholderItem");
    }
}
