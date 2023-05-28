using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject TimeOutPanel;
    private Scene scene;
    private bool inTimeOut = false;

    private AudioSource aSource;

    private float Level2Timer = 600.0f;

    public AudioClip backGroundMusic;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        if (backGroundMusic != null)
        {
            aSource.clip = backGroundMusic;
            aSource.Play();
        }
        scene = SceneManager.GetActiveScene();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && scene.name != "MainMenu" && scene.name != "Help" && inTimeOut==false)
        {
            
            TimeOutGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && scene.name != "MainMenu" && scene.name != "Help" && inTimeOut == true)
        {
            ContinueTimeGame();
            return;
        }

        if (scene.name == "level3")
        {
            //Debug.Log("X");
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemy.Length == 1)
            {
                /*Debug.Log("Y");*/
                GameOver(true);
            }
            /*else 
            {
                Debug.Log(enemy.Length);
            }*/
        }

        if (scene.name == "level2")
        {
            if (Level2Timer > Mathf.Epsilon)
            {
                Level2Timer -= Time.deltaTime;
            }
            else
            {
                GameOver(false);
            }    
        
        }
    }

    public void GameOver(bool playerWin) 
    {
        StartCoroutine(DelayGameOver(playerWin));
    }

    IEnumerator DelayGameOver(bool playerWin)
    {
        yield return new WaitForSeconds(0.5f);
        if (playerWin)
        {
            if (winAudio != null)
            {
                aSource.clip = winAudio;
                aSource.Play();
            }
            winPanel.SetActive(true);

        }
        else
        {
            if (loseAudio != null)
            {
                aSource.clip = loseAudio;
                aSource.Play();
            }
            losePanel.SetActive(true);
        }
        Time.timeScale = 0;

    }

    public void LoadNextLevel(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TimeOutGame() 
    {
       
        inTimeOut = true;
        
        TimeOutPanel.SetActive(true);
        
        Time.timeScale = 0;
        

    }

    public void ContinueTimeGame() 
    {
        
        inTimeOut = false;
        TimeOutPanel.SetActive(false);
        Time.timeScale = 1;

    }
}
