using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{

    public float time;
    public float starttime;

    private bool checkback;
    private Animator anim;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
            StopAllCoroutines();
            Attack();
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);

        }

    }

    void Attack()
    {
        if (checkback == true)
        {
            anim.SetBool("isWalking", false);


            //anim1.SetBool("Attacking", true);

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
        //coll2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("X");
        //coll2D.enabled = false;
        //anim1.SetBool("Attacking", false);

        if (checkback == true)
        {
            yield return new WaitForSeconds(5.0f);
            Attack();
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
