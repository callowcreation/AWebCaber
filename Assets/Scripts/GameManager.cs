using System.Collections;
using UnityEngine;
using static Values;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BalloonManager m_BalloonManager = null;
    [SerializeField]
    SoundManager m_SoundManager = null;
    [SerializeField]
    EffectsManager m_EffectsManager = null;

    [SerializeField]
    int m_PopCount = 0;
    [SerializeField]
    int m_PopsForSpecial = 10;

    IEnumerator Start()
    {
        while(true)
        {

            yield return null;
        }
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
                if (balloon.hits == balloon.maxHits)
                {
                    m_PopCount++;
                    if(m_PopCount % m_PopsForSpecial == 0)
                    {
                        m_BalloonManager.SpawnBalloon(BalloonTypes.Special);
                    }
                }
            }
        }
    }
}
