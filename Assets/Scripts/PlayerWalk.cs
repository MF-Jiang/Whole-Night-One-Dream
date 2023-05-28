using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Vector2 movDir = new Vector2();

    //public Transform target;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
        UpdateState();
    }

    private void UpdateState()
    {
        if (Mathf.Approximately(movDir.x, 0.0f) && Mathf.Approximately(movDir.y, 0.0f))
        {
            //anim.SetFloat("xDir", movDir.x);
            anim.SetBool("isWalking", false);
        }
        else
        {
            //anim.SetFloat("xDir", movDir.x);
            /*if (anim.GetBool("Die") == false)
            {
                anim.SetBool("isWalking", true);
            }
            else 
            {
                anim.SetBool("isWalking", false);
            }*/
            anim.SetFloat("xDir", movDir.x);
            anim.SetFloat("yDir",movDir.y);
            anim.SetBool("isWalking", true);
        }


    }

    private void FixedUpdate()
    {
        MoveCharacter(); 
    }

    private void MoveCharacter()
    {
        movDir.x = Input.GetAxisRaw("Horizontal");
        movDir.y = Input.GetAxisRaw("Vertical");
        movDir.Normalize();

        //transform.position += new Vector3(movDir.x, movDir.y, 0.0f) * moveSpeed * Time.deltaTime;

        //transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        //transform.Translate(movDir * moveSpeed * Time.deltaTime);

        // Debug.Log("XXXXXX" + movDir.ToString());

        //rb2d.AddForce(new Vector2(movDir.x, movDir.y));

        //rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y) + movDir * moveSpeed * Time.fixedDeltaTime);

        //rb2d.position += movDir * moveSpeed * Time.fixedDeltaTime;

        rb2d.velocity = moveSpeed * movDir;

    }
}
