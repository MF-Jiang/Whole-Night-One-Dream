using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 4;
    GameObject[] slots = new GameObject[numSlots];
    Item[] items = new Item[numSlots];
    Image[] itemImages = new Image[numSlots];
    Text[] slotTxts = new Text[numSlots];
    Button[] slotBtns = new Button[numSlots];
    private Player player;

    public AudioClip UseBottleAdio;
    public AudioClip UseBallAdio;
    protected AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CreateSlots();
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInputBag();
    }

    private void CreateSlots() {
        if (slotPrefab != null) {
            for (int i = 0; i < numSlots; i++) {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;
                itemImages[i] = slots[i].GetComponent<Slot>().itemImage;
                slotTxts[i] = slots[i].GetComponent<Slot>().qtyText;
                slotBtns[i] = slots[i].GetComponent<Slot>().slotBtn;


            }
        }
    }

    public void AddItem(Item itemToAdd) {
        for (int i = 0; i < numSlots; i++) {
            if (items[i] != null && items[i].itemType == itemToAdd.itemType) {
                if (items[i].stackable == true)
                {
                    items[i].quantity += 1;
                    slotTxts[i].text = items[i].quantity.ToString();
                    return;

                }
                else {
                    return;
                }
            }
        
        }

        for (int i = 0; i < numSlots; i++) {
            if (items[i] == null) {
                items[i] = Instantiate(itemToAdd);
                items[i].quantity += 1;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                if (items[i].stackable == true)
                {
                    slotTxts[i].text = items[i].quantity.ToString();
                    return;

                }
                else {
                    return;                
                }
            }

        }

        return;
        
    }

    private void DropItem(int index) {
        if (items[index] == null)
        {
            return;
        }
        else {
            if (items[index].stackable == true)
            {
                items[index].quantity -= 1;
                slotTxts[index].text = items[index].quantity.ToString();
                switch (items[index].itemType)
                {
                    case Item.ItemType.HEALTH:
                        if (UseBottleAdio != null)
                        {
                            aSource.clip = UseBottleAdio;
                            aSource.Play();
                        }
                        player.AdjustHealthPoints(20);
                        break;
                    case Item.ItemType.STAMINA:
                        if (UseBottleAdio != null)
                        {
                            aSource.clip = UseBottleAdio;
                            aSource.Play();
                        }
                        player.AdjustStaminaPoints(30);
                        break;
                    default:
                        break;
                }
                if (items[index].quantity <= 0)
                {
                    items[index] = null;
                    itemImages[index].sprite = null;
                    itemImages[index].enabled = false;
                    slotTxts[index].text = "";
                }
            }
            else
            {
                //items[index].quantity -= 1;
                if (UseBallAdio != null)
                {
                    aSource.clip = UseBallAdio;
                    aSource.Play();
                }
                Weapon weaponScript = player.GetComponent<Weapon>();
                weaponScript.ChangeAmmoPool(items[index]);
            }

        }
    }
    private void KeyInputBag()
    {
        int temp;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            temp = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            temp = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            temp = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            temp = 3;
        }
        else
        {
            return;
        }
        DropItem(temp);
    }
}


