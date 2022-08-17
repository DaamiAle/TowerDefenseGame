using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Attributes")]
    public static Turret Instance;
    public Transform Shootpoint;
    public GameObject bullet;

    [Header("Turret Properties")]
    public int Lifes = 5;
    public int CurrentLifes = 5;
    public float ViewDistance;
    public float FireRate;
    public float Force;
    public float Speed = 1.5f;
    public bool IsChildTurret = false;

    private float NextTimeToFire = 0;
    private Transform Target;
    private Vector3 CloseEnemyRef;
    private Vector2 Direction;
    private bool Detected = false;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            Vector2 targetPosition = Target.position;
            Direction = targetPosition - (Vector2)transform.position;
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, ViewDistance);
            if (rayInfo)
            {
                if (rayInfo.collider.gameObject.tag == "Unit")
                {
                    if (!Detected)
                    {
                        Detected = true;
                    }
                }
                else
                {
                    if (Detected)
                    {
                        Detected = false;
                    }
                }
            }
            if (Detected)
            {
                if (Time.time > NextTimeToFire)
                {
                    NextTimeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }
        }
    }
    void shoot()
    {
        //LevelManager.instance.CalculateCriticalFactor();
        GameObject bulletInst = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        bulletInst.GetComponent<Rigidbody2D>().AddForce(Direction * Force * Speed);
        //LevelManager.instance.Damage = LevelManager.instance.oldDamage;
    }
    private void OnDrawGizmosSelected()
    {
        
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Unit");
        float shortDistance = Mathf.Infinity;
        GameObject closeEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                closeEnemy = enemy;
                CloseEnemyRef = enemy.transform.position;
            }
        }
        if (closeEnemy != null && shortDistance <= ViewDistance)
        {
            Target = closeEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }
}
