using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;               //brzina rotacije
    [SerializeField] float jumpSpeed;                   //brzina skoka
    [SerializeField] int maxNumOfJumps;                 //maksimalni broj skokova --> double jump
    [SerializeField] float jumpHorizontalSpeed;         //horizontalna brzina tijekom skoka

    private int numJump;                                //varijabla koja se povecava pritiskom na tipku SPACE
    private CharacterController characterController;    //komponenta igraca
    private float ySpeed;                               //gravitacija
    private Vector3 velocity;                           //vektor za brzinu
    private Vector3 movementDirection;                  //vektor smjera kretanja
    private float originalStepOffset;                   //dohvacanje originalnog StepOffseta

    //Animation
    private Animator animator;                          //komponenta na igracu
    private bool isJumping;                             //ako si skocio == TRUE
    private bool isGrounded;                            //ako si na podlozi == TRUE

    void Start()
    {
        animator = GetComponent<Animator>();                       // dohvacanje komponente Animator na igracu
        characterController = GetComponent<CharacterController>(); // dohvacanje characterController komponenter igraca
        originalStepOffset = characterController.stepOffset;       // originalni Step offset = 0.3
    }

    void Update()
    {
        //dohvacanje pritisnutih tipki i spremanje u varijable
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // smjer kretanja igraca
        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();


        //gravitacija
        ySpeed += Physics.gravity.y * Time.deltaTime;

        //na podu ili nisi iskoristio sve skokove
        if(characterController.isGrounded || numJump <= maxNumOfJumps)
        {
            //ako je na podu treba postaviti numJump - kako bi i dalje radilo
            if(characterController.isGrounded){
                //vracanje step offseta na original kako bi mogli i dalje hodati po stepenicama
                characterController.stepOffset = originalStepOffset;
                ySpeed = -.5f;
                numJump = 1;
                animator.SetBool("IsGrounded", true);
                isGrounded = true;
                animator.SetBool("IsJumping", false);
                isJumping = false;
                animator.SetBool("IsFalling", false);
            }
            //ako je pritisnut SPACE
            if(Input.GetButtonDown("Jump"))
            {
                numJump++;
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                animator.SetBool("IsGrounded", false);
                isGrounded = false;
                AudioManagerScript.Instance.PlayJumpSound();

            }
        }
        else
        {
            characterController.stepOffset = 0f;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && ySpeed < 0) || ySpeed < -2){
                animator.SetBool("IsFalling", true);
            }
        }

        if(ySpeed < -2){
            animator.SetBool("IsFalling", true);
        }

        // ako se igrac krece, okrece se prema smjeru kretanja
        if (movementDirection != Vector3.zero)                                                                              
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsFalling", false);
            // Quaternion - varijabla za spremanje rotacija, LookRotaion za spremanje pogleda rotacije(napred, gore(y))
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            //mjenjanje rotacije igraca (trenutna, zeljenja, brzina)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); 
        }else{
            animator.SetBool("IsMoving", false);
        }

        //ako je igrac skocio
        if(!isGrounded){
            Vector3 velocity = movementDirection * jumpHorizontalSpeed;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);
            animator.SetBool("IsFalling", true);
        }
    }

    //animator za kretanje
   private void OnAnimatorMove() {
    if(isGrounded){
        velocity = animator.deltaPosition;
        velocity.y = ySpeed * Time.deltaTime;
        characterController.Move(velocity);
    } 
    }
}

