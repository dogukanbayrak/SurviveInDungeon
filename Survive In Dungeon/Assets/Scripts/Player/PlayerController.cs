using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxSpeed = 5f;
    public float turnSpeed = 15;

    private Animator anim;
    private CharacterController Controller;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private Vector3 playerMove = Vector3.zero;
    private Vector3 targetMovePoint = Vector3.zero;

    private float currentSpeed;
    private float playerToPointDistance;
    private float gravity = 9.8f;
    private float height;

    private bool canMove;
    private bool finishedMovement = true;
    private Vector3 NewMovepoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        currentSpeed = maxSpeed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();


            
        
    }

    bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }

    void CalculateHeight()
    {
        if (IsGrounded())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    void CheckIfFinishedMovement()
    {
        if (!finishedMovement)
        {
            if(!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.8f)
            {
                finishedMovement = true;
            }
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Equip Weapon"))
            {
                //MovePlayer();
                //Controller.Move(playerMove);

                MovePlayer();
                playerMove.y = height * Time.deltaTime;
                collisionFlags = Controller.Move(playerMove);
            }
        }
    }


    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if(playerToPointDistance >= 1f) 
                    { 
                        canMove = true; 
                        targetMovePoint= hit.point;


                    }
                }
            }            
        }

        if(canMove)
        {

            anim.SetBool("canWalk", true);
            
            NewMovepoint = new Vector3(targetMovePoint.x, transform.position.y, targetMovePoint.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(NewMovepoint - transform.position), turnSpeed * Time.deltaTime);

            playerMove = transform.forward * currentSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position,NewMovepoint)<= 0.6f)
            {
                canMove=false;
                
            }

        }
        else
        {
            anim.SetBool("canWalk", false);
            playerMove.Set(0f, 0f, 0f);
        }

    }


    public bool FinishedMovement { 
        get 
        {
            return finishedMovement;  
        } 
        set 
        { 
            finishedMovement = value;  
        }
    }

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    public Vector3 TargetPosition { get {  return targetMovePoint; } set { targetMovePoint = value; } }


}
