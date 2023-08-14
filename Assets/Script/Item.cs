using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName ="Inventory/New item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite itemImage;
}
