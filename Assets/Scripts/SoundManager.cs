using UnityEngine;
using static Values;

public class SoundManager : MonoBehaviour
{

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
    [SerializeField]
    AudioClip[] m_PopSpecialClips = null;

    AudioSource m_AudioSource = null;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Balloon.OnContact -= Balloon_OnContact;
        Balloon.OnContact += Balloon_OnContact;
    }

    void OnDisable()
    {
        Balloon.OnContact -= Balloon_OnContact;
    }

    void Balloon_OnContact(object sender, Collision2D collision)
    {
        Balloon balloon = (Balloon)sender;
        if (collision.gameObject.layer == Values.PLAYER_LAYER)
        {
            if (collision.collider is CapsuleCollider2D)
            {
                Play(Surface.Head);
                if (balloon.hits == balloon.maxHits)
                {
                    if (balloon.balloonType == BalloonTypes.Normal)
                    {
                        PlayPop();
                    }
                    else
                    {
                        PlayPopSpecial();
                    }
                }
            }
            else
            {
                Play(Surface.Body);
            }
        }
        else if (collision.gameObject.layer == GROUND_LAYER)
        {
            Play(Surface.Ground);
        }
        else if (collision.gameObject.layer == BALLOON_LAYER)
        {
            Play(Surface.Balloon);
        }
    }

    void PlayPop()
    {
        if (m_PopClips.Length > 0)
        {
            AudioClip clip = m_PopClips[Random.Range(0, m_PopClips.Length)];
            m_AudioSource.PlayOneShot(clip);
        }
    }

    void PlayPopSpecial()
    {
        if (m_PopSpecialClips.Length > 0)
        {
            AudioClip clip = m_PopSpecialClips[Random.Range(0, m_PopSpecialClips.Length)];
            m_AudioSource.PlayOneShot(clip);
        }
    }

    void Play(Surface surface)
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
