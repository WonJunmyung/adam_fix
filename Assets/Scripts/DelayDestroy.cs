using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        if (GameDataManager.Instance.isSound == 1)
        {
            this.GetComponent<AudioSource>().Play();
        }
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
