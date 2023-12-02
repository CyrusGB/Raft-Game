using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Items/New Item", order = 1)]
public class ItemObj : ScriptableObject
{
    public string itemName;
    [TextArea(0,5)]public string itemDesc;
    public Sprite itemSprite;
    public int maxStackSize = 30;
}
