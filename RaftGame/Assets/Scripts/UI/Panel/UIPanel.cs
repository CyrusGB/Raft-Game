using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected bool isOpen = false;
    public virtual void ChangeOpenState(bool newBool){
        isOpen = newBool;
        this.gameObject.SetActive(false);
    }
}