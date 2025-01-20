using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNormal : MonoBehaviour
{
    public float speed;
    [SerializeField]
    float range;
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;
    [SerializeField]
    float maxDistance;


    public LayerMask whatIsGround;
    bool walkPointSet;
    Vector2 wayPoint;
    public int hpMax;
    private int hp;
    public GameObject TakeFishUI;
    private bool onTake;
    public static bool checkFishEat;

    [SerializeField]
    private string _tag;
    public int slowDown;
    public int point;
    public int time;

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        this.tag = _tag;
        checkFishEat = true;
        xPos = Random.Range(-9.5f, 9.5f);
        yPos = Random.Range(-1.0f, -2.5f);
        transform.position = new Vector2(-xPos, yPos);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        SetNewDestination();


    }

    void Update()
    {


        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        Vector3 direction = (Vector3)wayPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }

        if (onTake)
        {
            Invoke("TakeOffFish", 1);
        }



    }


    void SetNewDestination()
    {

        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-1.0f, -2.5f));

    }


    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Pod") /*&& checkFishEat*/)
        {


            HookFish.hpFish = hp;
            Debug.Log(hp + "Give");
            checkFishEat = false;
        }

    }

    public void TakeOnFish()
    {
        Cursor.lockState = CursorLockMode.None;
        TakeFishUI.SetActive(true);
        GameManager.fishScore += 10;
        Debug.Log("On");

        onTake = true;

    }

    public void TakeOffFish()
    {
        Cursor.lockState = CursorLockMode.None;
        TakeFishUI.SetActive(false);
        Debug.Log("Off");

    }
}
