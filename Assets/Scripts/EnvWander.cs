using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvWander : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public float waitTime;

    public Animator anim;
    //private PolygonCollider2D coll2D;

/*    public float time;
    public float starttime;*/

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;

    public bool followPlayer = true;
    private Transform playerTransform = null;


    // Start is called before the first frame update
    public void Start()
    {
        //base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
        anim = gameObject.GetComponent<Animator>();

        //coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        //base.Update();
        Flip();

        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        anim.SetBool("isWalking", true);

        if (Vector2.Distance(transform.position, movePos.position) < Mathf.Epsilon)
        {
            anim.SetBool("isWalking", false);
            if (waitTime <= Mathf.Epsilon)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }


    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Vector2.Distance(transform.position, movePos.position) > float.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (movePos.position.x - transform.position.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (movePos.position.x - transform.position.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("findPlayer") && followPlayer == true)
        if (collision.gameObject.CompareTag("Player") && followPlayer == true&&collision.GetType()==typeof(CircleCollider2D))
        {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            movePos.position = playerTransform.position;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("findPlayer") && followPlayer == true)
        if (collision.gameObject.CompareTag("Player") && followPlayer == true && collision.GetType() == typeof(CircleCollider2D))
        {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            movePos.position = playerTransform.position;
            speed = 2 * speed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("findPlayer") && followPlayer == true)
        if (collision.gameObject.CompareTag("Player") && followPlayer == true && collision.GetType() == typeof(CircleCollider2D))
        {
            speed = speed / 2;
        }
    }
}
