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
    int m_SpecialPopCount = 0;
    [SerializeField]
    int m_PopsForSpecial = 10;

    bool m_HasSpecialBalloon = false;

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

        Balloon.OnOutOfBounds -= Balloon_OnOutOfBounds;
        Balloon.OnOutOfBounds += Balloon_OnOutOfBounds;
    }

    void OnDisable()
    {
        Balloon.OnContact -= Balloon_OnContact;

        Balloon.OnOutOfBounds -= Balloon_OnOutOfBounds;
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
                    if(balloon.balloonType == BalloonTypes.Normal)
                    {
                        if (m_HasSpecialBalloon == false)
                        {
                            m_SpecialPopCount++;
                            if (m_SpecialPopCount % m_PopsForSpecial == 0)
                            {
                                m_BalloonManager.SpawnBalloon(BalloonTypes.Special);
                                m_HasSpecialBalloon = true;
                            }
                        }
                    }
                    else
                    {
                        m_SpecialPopCount = 0;
                        m_HasSpecialBalloon = false;
                        m_EffectsManager.DestroyFlowers();
                    }
                }
            }
        }
    }
    void Balloon_OnOutOfBounds(object sender, System.EventArgs e)
    {
        Balloon balloon = (Balloon)sender;
        if(balloon.balloonType == BalloonTypes.Special)
        {
            m_SpecialPopCount = 0;
            m_HasSpecialBalloon = false;
            m_EffectsManager.DestroyFlowers();
        }
    }
}
