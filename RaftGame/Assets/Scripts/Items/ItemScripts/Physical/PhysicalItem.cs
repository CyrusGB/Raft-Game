using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : Item
{
    [SerializeField] SpriteRenderer rend;
    public override void SetSprite(){
        rend.sprite = _scrObj.itemSprite;
    }
}
