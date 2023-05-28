using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate1Attack : Character
{
    public int damage;
    public float time;
    public float starttime;
    private Animator anim1;
    private PolygonCollider2D coll2D;

    //private CircleCollider2D circle;



    private bool checkback;


    // Start is called before the first frame update
    void Start()
    {
        anim1 = gameObject.transform.parent.GetComponentInParent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            checkback = true;
            Attack();

        }

    }

    void Attack() {
        if (checkback == true) {
            anim1.SetBool("couldAttack", true);

            if (StartAttack() != null)
            {
                StopCoroutine(StartAttack());
            }
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        if (disableHitBox() != null)
        {
            StopCoroutine(disableHitBox());
        }
        
        yield return new WaitForSeconds(starttime);
        coll2D.enabled = true;
        StartCoroutine(disableHitBox());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("X");
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }

    IEnumerator disableHitBox() {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
        anim1.SetBool("couldAttack", false);

        if (checkback == true) {
            yield return new WaitForSeconds(5.0f);
            Attack();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            checkback = false;
            //Debug.Log(1);
        }
    }
}
