using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BalloonManager;

public class SoundManager : MonoBehaviour
{

    static SoundManager s_Instance = null;

    public static SoundManager instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = FindObjectOfType<SoundManager>();
            }
            return s_Instance;
        }
    }

    [Header("Sound FX")]
    [SerializeField]
    AudioClip[] m_GoundClips = null;
    [SerializeField]
    AudioClip[] m_BodyClips = null;
    [SerializeField]
    AudioClip[] m_HeadClips = null;
    [SerializeField]
    AudioClip[] m_BalloonClips = null;

    [Header("Pop SFX")]
    [SerializeField]
    AudioClip[] m_PopClips = null;

    AudioSource m_AudioSource = null;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayPop()
    {
        if (m_PopClips.Length > 0)
        {
            AudioClip clip = m_PopClips[Random.Range(0, m_PopClips.Length)];
            m_AudioSource.PlayOneShot(clip);
        }
    }

    public void Play(Surface surface)
    {
        AudioClip clip = null;
        switch (surface)
        {
            case Surface.Ground:
                if (m_GoundClips.Length > 0)
                {
                    clip = m_GoundClips[Random.Range(0, m_GoundClips.Length)];
                }
                break;
            case Surface.Body:
                if (m_BodyClips.Length > 0)
                {
                    clip = m_BodyClips[Random.Range(0, m_BodyClips.Length)];
                }
                break;
            case Surface.Head:
                if (m_HeadClips.Length > 0)
                {
                    clip = m_HeadClips[Random.Range(0, m_HeadClips.Length)];
                }
                break;
            case Surface.Balloon:
                if (m_BalloonClips.Length > 0)
                {
                    clip = m_BalloonClips[Random.Range(0, m_BalloonClips.Length)];
                }
                break;
            default:
                break;
        }
        if (clip != null)
        {
            m_AudioSource.PlayOneShot(clip);
        }
    }
}
