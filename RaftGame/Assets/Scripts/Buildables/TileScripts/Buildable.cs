using UnityEngine;

public abstract class Buildable : MonoBehaviour
{
    [SerializeField] protected BuildableObj _scrObj;
    protected BuildingData _buildingData;
    public BuildableObj ReturnScrObj() => _scrObj;
    public BuildingData ReturnData() => _buildingData;
    public virtual void SetData(BuildingData newData){
        _buildingData = newData;
    }
    public virtual void CreateNewData(Vector2 newPos){
        SetData(new StaticBuildingData(newPos, _scrObj.bldName));
    }
}
