using UnityEngine;
using System;


public abstract class Item : MonoBehaviour
{
    protected ItemData _data;
    protected ItemObj _scrObj;
    protected int _stackSize = 0;
    public int slotIndex; 
    public ItemObj ReturnScrObj(){
        return _scrObj;
    }

    public virtual void SetStack(int amount){
        _stackSize = amount;
    }

    public abstract void SetSprite();

    public void SetItem(ItemObj newObj, ItemData newData, int index){
        _scrObj = newObj;
        _data = newData;
        slotIndex = index;
        SetStack(newData.stackSize);
        SetSprite();
    }

    public ItemData ReturnData()=> _data;

}


