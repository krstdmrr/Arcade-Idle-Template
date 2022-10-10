using UnityEngine;

public interface IDropable
{
    public void Drop(GameObject tObject);

    public ItemType ItemType { get; }
}