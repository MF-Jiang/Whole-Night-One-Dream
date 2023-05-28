using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float wanderIntervalTime=3.0f;
    public float wanderSpeed = 2.0f;
    public float pursuitSpeed = 2.5f;
    private float currentSpeed;
    private Rigidbody2D rd2d;

    private Coroutine moveCoroutine;

    private Vector2 endPointPosition;

    private Animator anim;
    private float remainingDistance;

    private CircleCollider2D circle;

    private Transform playerTransform=null;

    public bool followPlayer = true;

    // Start is called before the first frame update

    void Start()
    {
        rd2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        circle = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine(WanderCoroitine());
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    /*private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,circle.radius);
    }*/

    IEnumerator WanderCoroitine() {
        while (true) {
            ChooseEndPoint();
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCoroutine());
            yield return new WaitForSeconds(wanderIntervalTime);
        }
    }

    void ChooseEndPoint() 
    {
        float wanderAngle = Random.Range(0, 360);
        float wanderRadius = Random.Range(2,5);
        wanderAngle = wanderAngle * Mathf.Deg2Rad;
        endPointPosition = rd2d.position + new Vector2(Mathf.Cos(wanderAngle), Mathf.Sin(wanderAngle))*wanderRadius;

    }

    IEnumerator MoveCoroutine() {
        remainingDistance = (rd2d.position - endPointPosition).sqrMagnitude;
        
        while (remainingDistance>float.Epsilon) {

            anim.SetBool("isWalking", true);

            currentSpeed = wanderSpeed;

            if (playerTransform!=null) {
                endPointPosition = playerTransform.position;
                currentSpeed = pursuitSpeed;
            }

            Vector2 newPosition = Vector2.MoveTowards(rd2d.position,endPointPosition,currentSpeed*Time.fixedDeltaTime);
            rd2d.MovePosition(newPosition);
            //Debug.Log(Mathf.Abs(rd2d.velocity.x));
            remainingDistance = (rd2d.position - endPointPosition).sqrMagnitude;
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("isWalking", false);
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = remainingDistance > float.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (endPointPosition.x - rd2d.position.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (endPointPosition.x - rd2d.position.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Env"))
        {
            //Debug.Log("111");
            endPointPosition = rd2d.position;
            anim.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&followPlayer==true) {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            if (moveCoroutine!=null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&followPlayer==true)
        {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& followPlayer==true) {

            anim.SetBool("isWalking",false);
            currentSpeed = wanderSpeed;

            playerTransform = null;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                //moveCoroutine = null;
            }

        }
    }

}
