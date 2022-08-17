using UnityEngine;

public class CommandCenter : MonoBehaviour
{
    [Header("CommandCenter Attributes")]
    public static CommandCenter instance;
    public Transform Shootpoint;

    [Header("CommandCenter Properties")]
    public int Life = 50;
    public int CurrentLife = 50;
    public int Coins = 10;
    public int CurrentCoins = 10;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateCoins", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLife < 1)
        {
            CurrentLife = 0;
            Destroy(gameObject);
            //SceneManagement.instance.ChanceToMainMenuScene();
        }
    }
}
