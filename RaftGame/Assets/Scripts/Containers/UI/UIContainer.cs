using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class UIContainer : UIPanel
{
    [SerializeField] protected UISlot _slotPF;
    protected List<UISlot> _slots = new();
    [SerializeField] protected GridLayoutGroup _grid;
    [SerializeField] protected UIItem _itemPrefab;
    Container _cont;
    protected ItemDictionary _dictionary;

    public virtual void LoadItems(){
        if(_slots.Count > 0){
            foreach(UISlot slot in _slots){
                Destroy(slot.gameObject);
            }
            _slots.Clear();
        }
        
        _dictionary = ItemDictionary.Instance;
        StorageData storage = (StorageData)_cont.ReturnData();
        BldContainerObj obj = (BldContainerObj)_cont.ReturnScrObj();
        if(_slots.Count != _cont.ReturnSize()){
            for (int i = 0; i < obj.containerSize; i++){
                UISlot slot = Instantiate(_slotPF, _grid.transform);
                slot.name = ("Slot " + i);
                _slots.Add(slot);
                _slots[i].SetContainer(_cont, i);
            }
        }

        if(storage.inv.contents != null){
            for (int i = 0; i < storage.inv.contents.Count; i++){
                if(_slots[i].HasItem() == null){
                    if(storage.inv.contents[i].itemName != "empty"){
                        UIItem newItem = Instantiate(_itemPrefab, _slots[i].transform);
                        newItem.SetItem(_dictionary.ReturnItemFromDict(storage.inv.contents[i].itemName), storage.inv.contents[i], i);
                        _slots[i].GiveItem(newItem);
                    }
                }
            }
        }
        
    }

    public void SetContainer(Container newCont){
        _cont = newCont;
    }



}
