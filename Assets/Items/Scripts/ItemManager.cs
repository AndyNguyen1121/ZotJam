using UnityEngine;
using System.Collections.Generic;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<Item> overworldItems;
    public List<Item> hellItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void AddItem(GameObject item)
    {
        Item new_item = Instantiate(item, transform.position, transform.rotation, transform).GetComponent<Item>();
        new_item.Activate();
        // Update UI.
    }
}
