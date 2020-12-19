using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_PrefabsPop = null;

    [SerializeField]
    GameObject[] m_PrefabsParticles = null;

    [SerializeField]
    GameObject[] m_PrefabsFlowers = null;

    [SerializeField]
    GameObject[] m_FlowersPositions = null;

    int m_PositionIndex = -1;

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
                if(balloon.hits == balloon.maxHits)
                {
                    int index = (int)balloon.balloonType;
                    InstantiateEffect(m_PrefabsPop, index, balloon.transform.position);
                    InstantiateEffect(m_PrefabsParticles, index, balloon.transform.position);

                    if(balloon.balloonType == Values.BalloonTypes.Normal)
                    {
                        m_PositionIndex++;
                        if(m_PositionIndex < m_FlowersPositions.Length)
                        {
                            Vector3 position = m_FlowersPositions[m_PositionIndex].transform.position;
                            InstantiateEffect(m_PrefabsFlowers, Random.Range(0, m_PrefabsFlowers.Length), position);
                        }
                    }
                }
            }
        }
    }

    static void InstantiateEffect(GameObject[] prefabs, int index, Vector3 position)
    {
        if (index >= prefabs.Length) return;
        if (prefabs[index] == null) return;

        Instantiate(prefabs[index], position, Quaternion.identity);
    }
}
