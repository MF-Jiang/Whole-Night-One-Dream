using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueTrigger : HintTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            RPGGameManager.sharedInstance.GameOver(true);
        }
    }
}
