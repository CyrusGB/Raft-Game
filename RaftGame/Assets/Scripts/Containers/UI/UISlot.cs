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
        ItemData itemData = eventData.pointerDrag.GetComponent<Item>().ReturnData();
        _cont.ChangeItemInSlot(itemData, _indexInCont, itemData.indexInContainer);
    }

    public void SetContainer(Container newContainer, int newIndex){
        _cont = newContainer;
        _indexInCont = newIndex;
    }
}
