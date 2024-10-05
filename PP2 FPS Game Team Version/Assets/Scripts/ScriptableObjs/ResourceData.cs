using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Kingdom/Resource")]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public string resourceDescription;
    public ResourceType resourceType;
    public int baseValue;
    public float productionRate;

    public enum ResourceType
    { 
        Raw,
        Refined
    }
}
