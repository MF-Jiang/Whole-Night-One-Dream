using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public GameObject StartMove;//��������
    private void Awake()
    {
        if (StartMove != null)//������������
        {
            VideoPlayer videoPlayer = StartMove.GetComponent<VideoPlayer>();
            StartMove.SetActive(true);//Ĭ�϶���ر�
            //videoPlayer.frame = 100;//����ǰ100֡
            videoPlayer.loopPointReached += VideoPlayer_loopPointReached;//��Ӳ��Ž����¼�
            StartMove.transform.SetAsLastSibling();
            videoPlayer.Play();//������Ƶ
        }
        else
            Debug.LogError("�������� StartMove=null");
    }

    /// <summary>
    /// ���Ž����򲥷ŵ�ѭ���ĵ�ʱ��ִ��
    /// </summary>
    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        if (StartMove != null)
        {
            StartMove.SetActive(false);
            GameObject.Destroy(StartMove);
            StartMove = null;
        }
        else
          Debug.LogError("�������� ���Ž��� StartMove=null");
    }

}
