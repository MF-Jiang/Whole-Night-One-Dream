using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float healthPoints = 50.0f;
    public float maxHealthPoints = 100.0f;

    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    private Animator anim;

    private BoxCollider2D coll2D;
    private CircleCollider2D coll2Dc;
    //private BoxCollider2D coll2D2;

    public AudioClip hurtAudio;
    protected AudioSource aSource;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        anim = GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();
        coll2Dc = GetComponent<CircleCollider2D>();
        //coll2D2 = GetComponentInChildren<BoxCollider2D>();
    }

    public void Update()
    {
        if (healthPoints <= Mathf.Epsilon)
        {
            CharacterDie();
        }
    }
    virtual public void CharacterDie() 
    {
        coll2D.enabled = false;
        coll2Dc.enabled = false;
        //coll2D2.enabled = false;

        //Debug.Log(coll2D2.enabled);

        anim.SetBool("Die",true);
        //anim.SetBool("Die", false);

        Invoke("PlayerDestroy", 1.2f);
        //Destroy(gameObject);
    }

    public void TakeDamage(float damageAmount) 
    {
        if (hurtAudio!=null)
        {
            aSource.clip = hurtAudio;
            aSource.Play();
        }
        healthPoints -= damageAmount;
        FlashColor(flashTime);

    }


    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        //Debug.Log(originalColor);
        sr.color = originalColor;
    }

    void PlayerDestroy() 
    {
        Destroy(gameObject);
    }
}
