using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookFish : MonoBehaviour
{
   // public string[] tagsToCheck;
    public float speed, returnSpeed, range, stopRange;

    [HideInInspector]
    public Transform caster, collidedWith;
    private LineRenderer line;
    private bool hasCollided;
    public GameObject hook;
    public static int hpFish;

    public bool fishingEpic;
    public bool fishingLegendary;
    //public bool epic;
    //public bool legendary;

    Vector2 target;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 20f;
    [SerializeField] float grappleShootSpeed = 20f;

    [HideInInspector] public bool retracting = false;

    bool isGrappling = false;
    public int maxHP = 10;
    private int hp;
    

    void Start()
    {
        //epic = fishingEpic;
        //legendary = fishingLegendary;
        line = transform.Find("Line").GetComponent<LineRenderer>();
        

        fishingEpic = SaveData.fishingEpic;
        fishingLegendary = SaveData.fishingLegen;
        //hp = hpFish;
        //Debug.Log("Take" + hp);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);

            if (hasCollided)
            {

                // transform.LookAt(caster);
                var dist = Vector2.Distance(transform.position, caster.position);
                if (dist < 1)
                {
                    Destroy(gameObject);
                   
                    Debug.Log(dist);
                    Debug.Log(stopRange);
                }
            }
            else
            {
                //var dist = Vector2.Distance(transform.position, caster.position);
                //if (dist < 0.5f)
                //{
                //  //  Collision(null);
                //   // Destroy(gameObject);
                //    Debug.Log("OK");
                //}

                //var dist = Vector2.Distance(transform.position, caster.position);
                //if (dist < stopRange)
                //{
                //    Destroy(gameObject);
                //    Debug.Log("OK");
                //}
                

            }
                
          

            //}      
            //if (Vector2.Distance(transform.position, target) < 0.5f)
            //{
            //    // retracting = false;
            //    isGrappling = false;
            //  //  Destroy(gameObject);
            //    // line.enabled = false;
            //}
            if (collidedWith) { collidedWith.transform.position = transform.position; }


        }




        //else
        //{
        //    Destroy(gameObject);
        //}

        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {

            
            Debug.Log("GOO");
            StartGrapple();

        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, caster.position, grappleSpeed * Time.deltaTime);

           

            if (hasCollided)
            {
                

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                    
                    if (fishingEpic == false)
                        hpFish-= 1;
                    else
                        hpFish -= 4;

                    if (fishingLegendary == true)
                        hpFish -= 10;

                    Debug.Log(hpFish);
                    //Debug.Log(GameManager.hpFish);
                    if (hpFish <= 0)
                    {

                        //Vector2 grapplePosx = Vector2.Lerp(transform.position, caster.position, grappleSpeed * Time.deltaTime);
                        transform.position = caster.position;
                    }
                }

            }
            else
            { transform.position = grapplePos; }

              line.SetPosition(1, transform.position);
            if (Vector2.Distance(transform.position, caster.position) < 0.5f)
            {
                retracting = false;
                isGrappling = false;
                Debug.Log("Hi");
                //Destroy(gameObject);
               // line.enabled = false;
            }


        }


    }

    public void StartGrapple()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null)
        {






           // isGrappling = true;
            target = hit.point;
            line.enabled = true;
            
            //line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 5;

        //line.SetPosition(0, caster.position);
        //line.SetPosition(1, transform.position);

        //Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
           Vector2 newPos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
           transform.position = newPos;
            //line.SetPosition(0, transform.position);
            // line.SetPosition(0, newPos);
            yield return null;
        }

        //line.SetPosition(1, caster.position);
       // if(hp == 0)
         retracting = true;

    }

    public void ClickToTakeFish()
    {

        hp -= 1;
        Debug.Log(hp);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (!hasCollided && tagsToCheck.Contains(other.tag))
        //{
        //    Collision(other.transform);
        //    Debug.Log("HO");
        //}
    }

    void Collision(Transform col)
    {
        speed = returnSpeed;
        hasCollided = true;
        
        if (col)
        {
            transform.position = col.position;
            collidedWith = col;
        }
        
        //line.SetPosition(0, transform.position);


    }


}
