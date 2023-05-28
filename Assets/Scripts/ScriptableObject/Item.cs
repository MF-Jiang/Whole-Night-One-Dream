using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemName;
    public Sprite sprite;
    public int quantity;
    public float amount;
    public bool stackable;
    
    public enum ItemType { 
        HEALTH,
        STAMINA,
        FIREBALL,
        WATERBALL,
    }

    public ItemType itemType;

}
