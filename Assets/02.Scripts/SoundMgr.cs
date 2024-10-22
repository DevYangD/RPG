using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name; // �����̸�
    public AudioClip clip;
}

public class SoundMgr : MonoBehaviour
{
    static public SoundMgr instance;
    #region singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource[] audioSourceBgm;

    public string[] playSoundName;
    public string[] playSoundBGM;

    [SerializeField]
    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
        playSoundBGM = new string[audioSourceBgm.Length];
    }
    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for(int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ������Դϴ�");
                return;
            }
            Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�");
        }
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                for (int j = 0; j < audioSourceBgm.Length; j++)
                {
                    if (!audioSourceBgm[j].isPlaying)
                    {
                        playSoundBGM[j] = bgmSounds[i].name;
                        audioSourceBgm[j].clip = bgmSounds[i].clip;
                        audioSourceBgm[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ������Դϴ�");
                return;
            }
            Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�");
        }
    }
    public void StopAllSE()
    {
        for( int i = 0;i < audioSourceEffects.Length;i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            { 
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�");
    }

    public void StopBGM(string _name)
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            if (playSoundBGM[i] == _name)
            {
                audioSourceBgm[i].Stop();
                return;
            }
        }
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�");
    }
}
