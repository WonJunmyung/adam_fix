using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricate : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy"))
        {
            if (other.transform.parent == null)
            {
                gameManager.listEnemys.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                gameManager.listEnemys.Remove(other.transform.parent.gameObject);
                Destroy(other.transform.parent.gameObject);
            }
        }

    }
}
