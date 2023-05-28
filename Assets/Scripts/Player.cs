using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    public float staminaPoints = 50.0f;
    public float maxStaminaPoints = 100.0f;
    public Inventory playerInventory;

    public AudioClip PickBottleAdio;
    public AudioClip PickBallAdio;

    private void Start()
    {
        base.Start();
        aSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        base.Update();
    }
    public void TakeStaminaPoint(float deplete)
    {
        staminaPoints -= deplete;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickupObject")) {
            Item hitObject = collision.gameObject.GetComponent<Pickup>().item;
            if (hitObject != null) {
                switch (hitObject.itemType) {
                    case Item.ItemType.HEALTH:
                        if (PickBottleAdio!=null)
                        {
                            aSource.clip = PickBottleAdio;
                            aSource.Play();
                        }
                        //hitObject.quantity += 1;
                        playerInventory.AddItem(hitObject);
                        //AdjustHealthPoints(hitObject.amount);
                        break;

                    case Item.ItemType.STAMINA:
                        if (PickBottleAdio != null)
                        {
                            aSource.clip = PickBottleAdio;
                            aSource.Play();
                        }
                        playerInventory.AddItem(hitObject);
                        //hitObject.quantity += 1;
                        //AdjustStaminaPoints(hitObject.amount);
                        break;

                    case Item.ItemType.FIREBALL:
                        if (PickBallAdio != null)
                        {
                            aSource.clip = PickBallAdio;
                            aSource.Play();
                        }
                        playerInventory.AddItem(hitObject);
                        break;

                    case Item.ItemType.WATERBALL:
                        if (PickBallAdio != null)
                        {
                            aSource.clip = PickBallAdio;
                            aSource.Play();
                        }
                        playerInventory.AddItem(hitObject);
                        break;

                    default:
                        break;
                }
            }
            collision.gameObject.SetActive(false);
        }
    }

    public void AdjustHealthPoints(float amount) {
        healthPoints += amount;
        //Debug.Log("health: "+healthPoints.ToString());
    }

    public void AdjustStaminaPoints(float amount) {
        staminaPoints += amount;
        //Debug.Log("stamina: " + staminaPoints.ToString());
    }

    public override void CharacterDie()
    {
        base.CharacterDie();
        RPGGameManager.sharedInstance.GameOver(false);
    }
}
