using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public const int ammoCount = 3;
    public GameObject[] ammoObjectPrefab = new GameObject[ammoCount];
    
    //public GameObject ammoObjectPrefab;
    static List<GameObject> ammoPool;
    public int poolSize = 5;
    private Player player;

    private Animator anim;
    //private int SkillCheck=0;

    // Start is called before the first frame update

    private void Awake()
    {

        ammoPool = new List<GameObject>();
        

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoObjectPrefab[0]);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);

        }

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Skill1"))
        {
            //anim.SetTrigger("Skill");
            FireAmmo();
        }
    }

    private void FireAmmo()
    {
        float angle;
        Transform NearestEnemy = OnGetEnemy();
        //Debug.Log(NearestEnemy);
        if (NearestEnemy != null)
        {
            //Debug.Log("A");
            Vector2 EnemyPosition = new Vector2(NearestEnemy.position.x, NearestEnemy.position.y);
            Vector2 direction = EnemyPosition - new Vector2(transform.position.x, transform.position.y);
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else {
            //Debug.Log("B");
            angle = Random.Range(0,360);
        }
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        SpawnAmmo(transform.position, rotation);


    }

    GameObject SpawnAmmo(Vector3 location, Quaternion rotation)
    {
        
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                
                ammo.transform.position = location;
                ammo.transform.rotation = rotation;

                Ammo ammoScript = ammo.GetComponent<Ammo>();
                ammoScript.lifeWeight = player.staminaPoints / (3 * player.maxStaminaPoints);
                //Debug.Log(ammoScript.lifeWeight);
                

                if (ammoScript.lifeWeight < 0.08f)
                {
                    ammoScript.lifeWeight = 0.08f;
                }

                if (ammoScript.skillName=="NullBall")
                {
                    //Debug.Log(ammo);

                    return null;
                }
                else if (ammoScript.skillName == "FireBall")
                {
                    
                    ammoScript.lifeWeight = 0.2f;
                    if (player.staminaPoints <= 8.0f)
                    {
                        return null;
                    }
                    anim.SetTrigger("Skill");
                    player.staminaPoints -= 8;

                    if (player.staminaPoints <= Mathf.Epsilon)
                    {
                        player.staminaPoints = 0.0f;
                    }
                }
                else if (ammoScript.skillName == "WaterBall")
                {
                    if (player.staminaPoints <= 2.0f)
                    {
                        return null;
                    }
                    anim.SetTrigger("Skill");
                    player.staminaPoints -= 2;

                    if (player.staminaPoints <= Mathf.Epsilon)
                    {
                        player.staminaPoints = 0.0f;
                    }
                }
                
                ammo.SetActive(true);
                return ammo;
            }
        }
        return null;
    }


    public Transform OnGetEnemy()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy.Length == 0)
        {
            return null;
        }
        else {
            float distance_min =10.0f;    
            int id = -1;                   
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].activeSelf == true)
                {
                    float distance = Vector2.Distance(transform.position, enemy[i].transform.position);
                    if (distance < distance_min)
                    {
                        distance_min = distance;
                        id = i;
                    }
                }
            }
            if (id == -1) 
            {
                return null;
            }
            return enemy[id].transform;
        }
    }

    public void ChangeAmmoPool(Item ammoItem)
    {
        int index = 0;
        for (int i = 0; i < ammoCount; i++)
        {
            Item ammoScript = ammoObjectPrefab[i].GetComponent<Pickup>().item;
            if (ammoScript.itemType==ammoItem.itemType)
            {
                index = i;
                break;
            }
        }
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(ammoPool[i]);
        }
        ammoPool.Clear();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoObjectPrefab[index]);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);

        }
    }
}
