using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                DontDestroyOnLoad(instance.gameObject);
                if (instance != null)
                {
                    Debug.LogError("���� ������ " + typeof(T) + "�� Ȱ��ȭ �� �� �����ϴ�.");
                }
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }



    // Update is called once per frame
    void Update()
    {

    }
}

