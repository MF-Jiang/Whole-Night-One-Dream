using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybase : MonoBehaviour
{
    public float health;
    //public int damage;

    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    public GameObject bloodStainPrefab;

    private Vector3 finaladdress =new Vector3(-0.7f,-10.0f,0);

    public AudioClip hurtAudio;
    protected AudioSource aSource;

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= Mathf.Epsilon) 
        {
            if (bloodStainPrefab!=null) {
                //Debug.Log(bloodStainPrefab.name);
                if (bloodStainPrefab.name == "FinalLevel1")
                {
                    Instantiate(bloodStainPrefab, finaladdress, Quaternion.identity);
                }
                else 
                {
                    Instantiate(bloodStainPrefab, transform.position, Quaternion.identity);
                }
                
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {
        if (hurtAudio!=null)
        {
            aSource.clip = hurtAudio;
            aSource.Play();
        }

        health -= damage;
        FlashColor(flashTime);
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}
