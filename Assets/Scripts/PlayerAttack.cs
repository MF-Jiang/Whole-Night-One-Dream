using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float starttime;

    private Animator anim;

    //private BoxCollider2D box2D;

    private PolygonCollider2D coll2D;

    protected AudioSource aSource;
    public AudioClip PlayerAttackAudio;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.transform.parent.GetComponentInParent<Animator>();
        aSource = GetComponent<AudioSource>();
        coll2D = GetComponent<PolygonCollider2D>();

        //box2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack") && anim.GetBool("Die")!=true )
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {

                if (PlayerAttackAudio != null)
                {
                    aSource.clip = PlayerAttackAudio;
                    aSource.Play();
                }
                anim.SetTrigger("Attack");
                coll2D.enabled = true;


                if (StartAttack() != null)
                {
                    StopCoroutine(StartAttack());
                }
                StartCoroutine(StartAttack());
            }
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
        //anim.SetTrigger("Attack");

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision == box2D);
        //if (collision.gameObject.CompareTag("Enemy") && collision==box2D)
        if (collision.gameObject.CompareTag("Enemy") && collision.GetType() ==typeof(BoxCollider2D))
        {
            //Debug.Log(damage);

            collision.GetComponent<Enemybase>().TakeDamage(damage);

        }


    }


}