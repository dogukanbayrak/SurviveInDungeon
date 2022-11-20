using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;


    public float turnSpeed;
    [SerializeField] private float movementSpeed;
   
    private float playerToPointDistance;

    [SerializeField]  private bool canMove;

    private Vector3 targetMovePoint = Vector3.zero;
    private Vector3 newMovePoint = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        //Movement(); 
        Movementt();
    }

    void Movement()
    {
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + movementVector * Time.deltaTime * movementSpeed);
    }

    void Movementt()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (playerToPointDistance >= 1f && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    canMove = true;
                    targetMovePoint = hit.point;

                }
            }
        }
        if (canMove)
        {
            newMovePoint = new Vector3(targetMovePoint.x, targetMovePoint.y, targetMovePoint.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newMovePoint - transform.position), turnSpeed * Time.deltaTime);

            rb.MovePosition(newMovePoint * Time.deltaTime * movementSpeed);

            if (Vector3.Distance(transform.position, newMovePoint) <= 0.6f)
            {
                canMove = false;
            }

        }

    }
}
