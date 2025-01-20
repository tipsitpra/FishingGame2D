using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject hook;

    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public float speed = 5;
    public string objectHook = "Pod";
    public GameObject moveLeftRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
            moveLeft = false;
            moveRight = false;
       // Instantiate(hook, transform.position, transform.rotation);


    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;

    }
    public void PointerDownRight()
    {
        moveRight = true;
    }
    
    public void PointerUpRight()
    {
        moveRight = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        GameObject[] objectsCheck = GameObject.FindGameObjectsWithTag(objectHook);
        MoveP();

        hook.GetComponent<PodMove>().caster = transform;

        if (objectsCheck.Length == 1)
        {
            moveLeftRight.SetActive(false);

        }
        if (objectsCheck.Length == 0)
        {
            moveLeftRight.SetActive(true);

        }
    }

    public void Hook()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(objectHook);
        if (objectsWithTag.Length == 0)
        {
            
            Instantiate(hook, transform.position, transform.rotation);
        }
        

    }

   

    private void MoveP()
    {
        if(moveLeft)
        {
            horizontalMove = -speed;

        }
        else if(moveRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }



   

}
