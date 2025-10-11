using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public string name;
    public string description;
    public virtual void Activate()
    {
        Debug.Log(name + " has been activated");
    }
}
