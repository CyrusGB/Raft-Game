using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    Item _itemInSlot;
    Container _cont;
    int _indexInCont;

    public ItemObj HasItem(){
        if(_itemInSlot != null){
            return _itemInSlot.ReturnScrObj();
        }
        return null;
    }

    public void GiveItem(Item newItem){
        _itemInSlot = newItem;
    }

    public void OnDrop(PointerEventData eventData){
        ItemData newItem = eventData.pointerDrag.GetComponent<Item>().ReturnData();
        _cont.ChangeItemInSlot(newItem, _itemInSlot == null ? new ItemData("empty", 0, _indexInCont) : _itemInSlot.ReturnData());
    }

    public void SetContainer(Container newContainer, int newIndex){
        _cont = newContainer;
        _indexInCont = newIndex;
    }
}
