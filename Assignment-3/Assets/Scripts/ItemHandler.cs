using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/Item", order = 1)]
public class ItemHandler : ScriptableObject
{
    public string id;
    public string description;
    public GameObject prefab;
}
