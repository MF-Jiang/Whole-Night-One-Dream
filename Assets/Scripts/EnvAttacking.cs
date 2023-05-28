using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvAttacking : MonoBehaviour
{
    private Animator anim1;
    private PolygonCollider2D coll2D;

    public float time;
    public float starttime;

    private bool checkback;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        anim1 = gameObject.transform.parent.GetComponentInParent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(checkback);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            checkback = true;
            StopAllCoroutines();
            Attack();

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            checkback = true;
        }
    }

   void Attack()
    {
        if (checkback == true)
        {
            anim1.SetBool("isWalking", false);
            anim1.SetBool("Attacking", true);

            if (StartAttack() != null)
            {
                StopCoroutine(StartAttack());
                //StartAttack() = null;
            }
            if (disableHitBox() != null)
            {
                StopCoroutine(disableHitBox());
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

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("X");
        coll2D.enabled = false;
        anim1.SetBool("Attacking", false);

        if (checkback == true)
        {
            yield return new WaitForSeconds(5.0f);
            Attack();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && collision.GetType() == typeof(BoxCollider2D))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            checkback = false;
            //Debug.Log(1);
        }
    }
}
