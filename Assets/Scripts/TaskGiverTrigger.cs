using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGiverTrigger : HintTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            hintPanel.SetActive(true);
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            hintPanel.SetActive(false);
        }
    }
}
