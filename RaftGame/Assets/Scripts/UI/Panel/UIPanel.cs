using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour{
    public virtual void ClosePanel(){
        Destroy(this.gameObject);
    }
}