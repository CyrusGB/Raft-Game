using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public abstract class Container : InteractableBuildable
{
    protected int _containerSize = 0;
    protected UIContainer _panel;
    [SerializeField] protected UIContainer _panelPf;

    public override void SetData(BuildingData newData)
    {
        Debug.Log(newData);
        base.SetData(newData);
        if(ReturnScrObj() is BldContainerObj bld){_containerSize = bld.containerSize;}
    }
    public override void CreateNewData(Vector2 newPos){
        SetData(new StorageData(newPos, _scrObj.bldName));
    }

    protected virtual void OpenContainer(){
        if(_panel == null){
            _panel = (UIContainer)UIManager.Instance.AddPanel(_panelPf);
            _panel.SetContainer(this);
        }
        _panel.LoadItems();
    }

    public int ReturnSize() => _containerSize;
    public override void Interact()
    {
        base.Interact();
        OpenContainer();
    }

    // public void ChangeItemInSlot(Item newItem, int newIndex, int currentIndex){
        
    //     if(_buildingData is ContainerData cont){
    //         if(cont.inv.contents[newIndex].itemName != "empty"){

    //         }else{
    //             cont.inv.contents[newIndex] = newItem.ReturnData();
    //             cont.inv.contents[currentIndex] = new ItemData("empty", 0);
    //             _panel.LoadItems();
    //         }
            
    //     }
    // }

    public void ChangeItemInSlot(ItemData newItem, int newIndex, int oldIndex){
        if(_buildingData is ContainerData cont){
            if(newIndex < _containerSize){
                if(cont.inv.contents.Count < newIndex + 1){ //Nothing in slot. need to increase list size
                    for (int i = cont.inv.contents.Count -1; i < newIndex + 1; i++){
                        cont.inv.contents.Add(new ItemData("empty", 0, i));
                    }
                }
                if(cont.inv.contents[newIndex] is ItemData oldItem){ //Swap items
                    cont.inv.contents[oldIndex] = oldItem;
                    oldItem.indexInContainer = oldIndex;
                    cont.inv.contents[newIndex] = newItem;
                    newItem.indexInContainer = newIndex;
                }
            }
            _panel.LoadItems();
        }
    }

    #region  oldCode
    // [SerializeField] protected ContainerData _data;
    // [SerializeField] protected GridLayoutGroup _grid;
    // [SerializeField] protected UIPanel _uiPref;
    // protected UIPanel _ui;
    // public virtual void OpenContainer(){
    //     if(_data !=null){
    //         if(_ui == null){
    //             _ui = UIManager.Instance.AddPanel(_uiPref);
    //             if(_ui is UIContainer _uiCont){
    //                 _uiCont.LoadItems(this);
    //             }
    //         }
    //     }else{
    //         Debug.Log("Container contains no data!");
    //     }
    //     //Open ui element
    //     //Talk back and forth with player inventory manager to transfer items.

    // }

    // public void Start()
    // {
    //     //CreateNewData(new ContainerData());
    // }

    // public override void SetData(BuildingData newData){
    //     base.SetData(newData);
    //     _data = newData.container;
    // }

    // public override void Interact()
    // {
    //     base.Interact();
    //     OpenContainer();
    // }

    // public ContainerData ReturnContainerData() => _data;
    // public void SetContainerData(ContainerData newData){
    //     _data = newData;
    // }
    #endregion

    

}




