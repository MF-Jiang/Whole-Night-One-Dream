using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLablel;
    public Image faceImage;

    [Header("文件文本")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite face01, face02;

    bool textFinished;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
        index = 0;
    }

    private void OnEnable()
    {
        /*textLablel.text = textList[index];
         index++;*/
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;

        }
        
        if (Input.GetKeyDown(KeyCode.Z) && textFinished==true)
        {
            /*textLablel.text = textList[index];
            index++;*/

            StartCoroutine(SetTextUI());

        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        
        textLablel.text = "";

        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;

            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;

            default:
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            textLablel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
        index++;
    }
}
