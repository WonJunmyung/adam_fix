using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBomb : MonoBehaviour
{
    public float maxSize = 10.0f;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (GameDataManager.Instance.isSound == 1)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x < maxSize)
        {
            float sizeUp = Time.deltaTime * speed;
            this.transform.localScale += new Vector3(this.transform.position.x + sizeUp, this.transform.position.y + sizeUp, this.transform.position.z + sizeUp);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

