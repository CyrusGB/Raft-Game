using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIItem : Item
{
    [SerializeField] Image img;
    [SerializeField] UIStackCounter stackCounter;

    public override void SetSprite(){
        img.sprite = _scrObj.itemSprite;
    }

    public override void SetStack(int amount){
        _stackSize = amount;
        stackCounter.SetStack(amount);
    }
    
}
