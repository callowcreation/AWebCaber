using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopDestroy : MonoBehaviour
{

    void Awake()
    {
        SoundManager.instance.PlayPop();
    }

    // Called from the animator
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
