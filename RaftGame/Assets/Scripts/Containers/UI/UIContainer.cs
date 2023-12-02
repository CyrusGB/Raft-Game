using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class UIContainer : UIPanel //Revert this back to UIPanel if issues
{
    [SerializeField] protected UISlot _slotPF;
    protected List<UISlot> _slots = new();
    [SerializeField] protected GridLayoutGroup _grid;
    [SerializeField] protected UIItem _itemPrefab;
    Container _cont;
    protected ItemDictionary _dictionary;

    public virtual void LoadItems(){
        ContainerData container = (ContainerData)_cont.ReturnData();
        Debug.Log(container.inventory);
        if(_slots.Count > 0){
            foreach(UISlot slot in _slots){
                Destroy(slot.gameObject);
            }
            _slots.Clear();
        }
        
        _dictionary = ItemDictionary.Instance;
        BldContainerObj obj = (BldContainerObj)_cont.ReturnScrObj();
        if(_slots.Count != _cont.ReturnSize()){
            for (int i = 0; i < obj.containerSize; i++){
                UISlot slot = Instantiate(_slotPF, _grid.transform);
                slot.name = "Slot " + i;
                _slots.Add(slot);
                _slots[i].SetContainer(_cont, i);
            }
        }

        if(container.inventory == null){
            container.inventory = new(new());
        }
        
        for (int i = 0; i < container.inventory.contents.Count; i++){
            UIItem newItem = Instantiate(_itemPrefab, _slots[container.inventory.contents[i].indexInContainer].transform);
            newItem.SetItem(_dictionary.ReturnItemFromDict(container.inventory.contents[i].itemName), container.inventory.contents[i], i);
            _slots[container.inventory.contents[i].indexInContainer].GiveItem(newItem);

            #region oldcode
            // for (int i = 0; i < storage.inventory.contents.Count; i++){
            //     if(_slots[i].HasItem() == null){
            //         if(storage.inventory.contents[i] != null){
            //             UIItem newItem = Instantiate(_itemPrefab, _slots[i].transform);
            //             newItem.SetItem(_dictionary.ReturnItemFromDict(storage.inventory.contents[i].itemName), storage.inventory.contents[i], i);
            //             _slots[i].GiveItem(newItem);
            //         }
            //     }
            // }
            #endregion
        }
        
    }

    public void SetContainer(Container newCont){
        _cont = newCont;
    }



}
