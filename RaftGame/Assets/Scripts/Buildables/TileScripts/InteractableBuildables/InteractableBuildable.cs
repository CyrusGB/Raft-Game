using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBuildable : Buildable
{
    public virtual void Interact(){
        Debug.Log("Interacted with: " + _scrObj.bldName);
    }
}
