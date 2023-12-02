using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private Buildable _selectedBld;
    public InventoryState InvState;
    UIInventory _panel;
    public void SelectBuildable(Buildable selection){
        _selectedBld = selection;
    }

    public void UpdateInventoryState(InventoryState newState){
        InvState = newState;
        switch (newState){
            case InventoryState.InventoryOpen:
                break;
            case InventoryState.InventoryClosed:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public enum InventoryState{
        InventoryOpen,
        InventoryClosed,
    } 

    public void PlaceBuildable(InputAction.CallbackContext cxt){
        if(cxt.performed && CheckIfPlayerInRange()){
            //Debug.Log(player.ReturnMousePos());
            GridManager.Instance.PlaceBuildable(_selectedBld, player.ReturnMousePos());
        }
    }

    public void DestroyBuildable(InputAction.CallbackContext cxt){
        if(cxt.performed && CheckIfPlayerInRange()){
            GridManager.Instance.DestroyBuildable(player.ReturnMousePos());
        }
    }

    public void Interact(InputAction.CallbackContext cxt){
        if(cxt.performed && CheckIfPlayerInRange()){
            GridManager.Instance.InteractWith(player.ReturnMousePos());
        }
    }

    public bool CheckIfPlayerInRange(){
        if(Mathf.Abs(player.ReturnPos().x - player.ReturnMousePos().x) <= player.ReturnReach() && Mathf.Abs(player.ReturnPos().y - player.ReturnMousePos().y) <= player.ReturnReach()){
            return true;
        }
        return false;
    }
}
