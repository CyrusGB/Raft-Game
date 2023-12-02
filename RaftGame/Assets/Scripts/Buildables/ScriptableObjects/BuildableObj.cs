using UnityEngine;
public abstract class BuildableObj : ScriptableObject
{
    public string bldName;
    [TextArea(0,5)]public string bldDesc;
    public Buildable bldPrefab;
    public Vector2 bldSize = new(1,1);
    public bool needsGrounded = true;
}