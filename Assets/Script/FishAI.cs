using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FishAI : MonoBehaviour
{
 
    public float speed;
    [SerializeField]
    float range;
    [SerializeField]
    float minYPos; 
    [SerializeField]
    float maxyYPos;
    [SerializeField]
    float maxDistance;


    public LayerMask whatIsGround;
    bool walkPointSet;
    Vector2 wayPoint;
    public int hpMax;
    public float hp;
    private Transform _Rod;
    public GameObject TakeFishUI;
    private bool onTake;
    public static bool checkFishEat;
    public int time;

    [SerializeField]
    private string _tag;
    public float slowDown;
    public int point;
    public GameObject hp_UIBoss;
    private SpriteRenderer spriteRenderer;


     void Awake()
    {
        _Rod = transform;
        this.tag = _tag;
        checkFishEat = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        SetNewDestination();
        float yPos = Random.Range(minYPos, maxyYPos);
        transform.position = new Vector2(Random.Range(-maxDistance, maxDistance), yPos);
        GameManager.fishHP = hpMax;
        GameManager.maxfishHP = hpMax;

    }

    void Update()
    {

        hp = GameManager.fishHP;
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        Vector3 direction = (Vector3)wayPoint - transform.position;
        float currentZ = transform.rotation.eulerAngles.z;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, currentZ);

        if (Vector2.Distance(transform.position, wayPoint) < range)
           {
                SetNewDestination();
           }

        

     }


        void SetNewDestination()
    {
        //xPos = Random.Range(9, -9);
         float yPos = Random.Range(minYPos, maxyYPos);

        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), yPos);


           // wayPoint = new Vector2(xPos, yPos);      
        
    }



    //private void OnCollisionEnter(Collision other)
    //{
    //    SetNewDestination();
    //}


    public void OnTriggerEnter2D(Collider2D other)
    {

        //if (other.CompareTag("Player"))
        //{

        //    spriteRenderer.enabled = !spriteRenderer.enabled;
        //    Destroy(gameObject, 0.5f);
        //    // if(!onTake)
        //    TakeOnFish();
        //    checkFishEat = true;
        //}
            
       // if (other.CompareTag("Pod") /*&& checkFishEat*/)
        //{
            //_Rod = this.transform;
            //_Rod.SetParent(transform);
            //HookFish.hpFish = hp;
            Debug.Log("POD");
            checkFishEat = false;
        //}

    }

    //public void TakeOnFish()
    //{
    //    Cursor.lockState = CursorLockMode.None;
    //    TakeFishUI.SetActive(true);
    //    GameManager.fishScore += 10;
    //    Debug.Log("On");
        
    //    onTake = true;
        
    //}

    //public void TakeOffFish()
    //{
    //    Cursor.lockState = CursorLockMode.None;
    //    TakeFishUI.SetActive(false);
    //    Debug.Log("Off");

    //}

 

}
