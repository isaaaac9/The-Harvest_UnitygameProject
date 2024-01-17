using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{


    private InputHandler input;
    private Animator anim;

    PlayerInteraction playerInteraction;

    [SerializeField] private float Movespeed;
    [SerializeField] private Camera camera;
    [SerializeField] private float rotateSpeed;


    private void Awake()
    {
        input = GetComponent<InputHandler>();
       
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        
    }

    private void Update()
    {
        
        var targetVector = new Vector3(input.InputVecter.x, 0, input.InputVecter.y);
        var movementVector = MoveTowardTarget(targetVector);
        RotateTowardMovementVector(movementVector);
        Interact();

    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) return;
        anim.SetBool("IsMoving", true);
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        anim.SetBool("IsMoving", false);
        var speed = Movespeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;

        if (TimeManager.Instance.Duration == 0)
        {
            input.enabled = false;
        }
    }

    public void Interact()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            playerInteraction.Interact();           //Interact
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();       //Interact Item
        }
        if(Input.GetButtonDown("Fire3"))            
        {
            playerInteraction.ItemKeep();           //Keep Item
        }

    }


}
