using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " added to the backpack!");

        // You can implement more logic here, like updating UI or triggering events
    }

    // You can add more methods for using or managing items in the backpack
}
