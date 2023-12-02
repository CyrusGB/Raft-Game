using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveData{
    //Dont put anything in here unless you want all other data types to have it too.
}

[Serializable]
public class WorldData : SaveData{
    public List<StaticBuildingData> buildings;
    // public List<StorageData> containers;
    public List<ContainerData> containers;
    public List<ItemProcessorData> processors;
    public Dictionary<Vector2, BuildingData> dictionary = new();
}

[Serializable]
public class PlayerData : SaveData{
    public Vector2 pos;
    InventoryData inv;
}

[Serializable]
public abstract class BuildingData : SaveData{
    public string bldName;
    public Vector2 pos;

    public BuildingData(Vector2 pos, string bldName){
        this.pos = pos;
        this.bldName = bldName;
    }
}

[Serializable]
public class StaticBuildingData : BuildingData
{
    public StaticBuildingData(Vector2 pos, string bldName) : base(pos, bldName)
    {
    }
}

[Serializable]
public abstract class ContainerData : BuildingData{
    public InventoryData inv;
    public ContainerData(Vector2 pos, string bldName, List<ItemData> contents = null) : base(pos, bldName){
        inv.contents = contents;
    }
}

[Serializable] 
public class InventoryData{
    public List<ItemData> contents = new();

    public InventoryData(List<ItemData> contents){
        this.contents = contents;
    }
}

[Serializable]
public class StorageData : ContainerData //Acts as a non abstract ContainerData
{
    public StorageData(Vector2 pos, string bldName, List<ItemData> contents = null) : base(pos, bldName, contents){
        
    }
    
}

[Serializable]
public class ItemProcessorData : ContainerData
{
    public float processTimeLeft;
    public ItemProcessorData(Vector2 pos, string bldName, List<ItemData> contents = null) : base(pos, bldName, contents){
    }
}

[Serializable]
public class ItemData{
    public string itemName;
    public int stackSize;
    public int indexInContainer;

    public ItemData(string itemName, int stackSize, int indexInContainer){
        this.itemName = itemName;
        this.stackSize = stackSize;
        this.indexInContainer = indexInContainer;
    }
}
// public class GridData : SaveData{
//     public List<BuildingData> gridList = new();
// }

// [Serializable]
// public class BuildingData : SaveData{
//     public Vector2 pos;
//     public string buildingName;
//     public ContainerData container;

//     public BuildingData(Vector2 newPos, string newName = "", ContainerData containerData = null){
//         pos = newPos;
//         buildingName = newName;
//         if(containerData == null){
            
//             container = containerData;
//         }
//     }
// }

// [Serializable]
// public class PlayerData : SaveData{
//     public Vector2 playerPosition = new(0,0);
// }

// [Serializable]
// public class ContainerData : SaveData{
//     public int containerSize;
//     public List<ItemData> contents = new();

//     public ContainerData(int newSize, List<ItemData> newContents = null){
//         containerSize = newSize;
//         contents = newContents;
//     }
// }

// [Serializable]
// public class ItemData : SaveData{
//     public string itemName;
//     public string itemNbt;
//     public int stackSize;

//     public ItemData(string newItemName = "", string newNbt = "", int newStack = 0){
//         itemName = newItemName;
//         itemNbt = newNbt;
//         stackSize = newStack;
//     }
// }

// public abstract class SaveData{

// }


