using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Player
    private CharacterController cc;
    [SerializeField]
    private float speed;
    private Vector3 movementVelocity;
    private PlayerController playerController;
    private float verticalVelocity;
    public float Gravity = -9.8f;
    private Animator animator;
    public int Coin;
    private float attackAnimationDuration;
    public float SlideSpeed = 9f;
    public GameObject GameoverPanel;

    //Enemy
    public bool isPlayer = true;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Transform targetPlayer;

    //Character State
    public enum CharacterState
    {
        Normal, Attacking, Dead, BeingHit, Slide
    }
    public CharacterState currentState;

    //Player Slides
    private float attackStartTime;
    public float attackSlideDuration = 0.4f;
    public float attackSlideSpeed = 0.5f;

    //Health
    private Health playerHealth;

    //Damage Caster
    private DamageCaster damageCaster;

    //DropItem
    public GameObject ItemToDrop;


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        damageCaster = GetComponentInChildren<DamageCaster>();
        if (!isPlayer)
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            targetPlayer = GameObject.FindWithTag("Player").transform;
            navMeshAgent.speed = speed;
        }
        else
        {
            playerController = GetComponent<PlayerController>();
        }
    }



    private void CalculatorMovement()
    {
        if (playerController.MouseButtonDown && cc.isGrounded)
        {
            SwitchStateTo(CharacterState.Attacking);
            return;
        } else if(playerController.SpaceKeyDown && cc.isGrounded)
        {
            SwitchStateTo(CharacterState.Slide);
            return;
        }
        movementVelocity.Set(playerController.HorizontalInput, 0f, playerController.VerticalInput);
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;
        movementVelocity *= speed * Time.deltaTime;
        animator.SetFloat("Speed", movementVelocity.magnitude);
        animator.SetBool("Air", !cc.isGrounded);
        if (movementVelocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movementVelocity);
    }
    private void CalculatorEnemyMovement()
    {

        float DistanceToTarget = Vector3.Distance(this.transform.position, targetPlayer.transform.position);
        if (DistanceToTarget > navMeshAgent.stoppingDistance)
        {
            navMeshAgent.SetDestination(targetPlayer.position);
            animator.SetFloat("Speed", 0.2f);
        }
        else if (DistanceToTarget < navMeshAgent.stoppingDistance)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);
            SwitchStateTo(CharacterState.Attacking);
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case CharacterState.Normal:
                if (isPlayer)
                {
                    CalculatorMovement();
     
                }

                else
                    CalculatorEnemyMovement();
                break;
            case CharacterState.Attacking:
                if (isPlayer)
                {
                    
                    if (Time.time < attackStartTime + attackSlideDuration)
                    {
                        movementVelocity = transform.forward * 0.1f;
                    }

                    if(playerController.MouseButtonDown && cc.isGrounded)
                    {
                        string CurrentClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                        attackAnimationDuration = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                        if(CurrentClipName != "LittleAdventurerAndie_ATTACK_03" && attackAnimationDuration > 0.5f && attackAnimationDuration < 0.7f)
                        {
                            playerController.MouseButtonDown = false;
                            SwitchStateTo(CharacterState.Attacking);
                            CalculatorMovement();
                        }
                    }
                }
                break;
            case CharacterState.Dead:
                return;
            case CharacterState.BeingHit:               
                break;
            case CharacterState.Slide:
                movementVelocity = transform.forward * Time.deltaTime * SlideSpeed;
                break;

        }

        if(isPlayer)
        {
            if (!cc.isGrounded)
            {
                verticalVelocity = Gravity;
            }
            else
                verticalVelocity = Gravity * 0.3f;
            movementVelocity += verticalVelocity * Vector3.up * Time.deltaTime; // Cho nhân vật cố định trên mặt đất
            cc.Move(movementVelocity);
            movementVelocity = Vector3.zero;
        }



    }

    public void SwitchStateTo(CharacterState NewState)
    {
        if (isPlayer)
        {
            playerController.ClearCache();
        }

        //Exitting
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                if(isPlayer)
                    GetComponent<PlayerVFXManager>().StopBlade();
                break;
            case CharacterState.Dead:
                return;
            case CharacterState.BeingHit:
                break;
            case CharacterState.Slide:
                break;
        }
        //Enter New State
        switch (NewState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                animator.SetTrigger("Attack");
                if (isPlayer)
                {
                    attackStartTime = Time.time;
                    this.gameObject.GetComponent<PlayerVFXManager>().PlayerSlash();
                }
                break;
            case CharacterState.Dead:
                cc.enabled = false;
                animator.SetTrigger("Dead");
                if(isPlayer)
                {
                    StartCoroutine(GameoverPanelTurn());
                }
                break;
            case CharacterState.BeingHit:
                animator.SetTrigger("BeingHit");
                BeingHitAnimationEnd();
                break;
            case CharacterState.Slide:
                animator.SetTrigger("Slide");
                break;


        }
        currentState = NewState;
    }

    public void AttackAnimationEnd()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void BeingHitAnimationEnd()
    {
        SwitchStateTo(CharacterState.Normal);
    }
    public void SlideAnimationEnd()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void ApplyDamage(int damage, Vector3 attackPos = new Vector3())
    {
        if(playerHealth != null)
        {
            playerHealth.ApplyDamage(damage);
        }
        if(!isPlayer)
        {
            GetComponent<EnemyVFXManager>().PlayBeingHit();
        }
        if(isPlayer)
        {
            SwitchStateTo(CharacterState.BeingHit);
        }
    }
    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }
    public void DisnableDamageCaster()
    {
        damageCaster.DisnableDamageCaster();
    }

    public void DropItem()
    {
        if(ItemToDrop != null)
        {
            Instantiate(ItemToDrop, this.transform.position, Quaternion.identity);
        }
    }

    public void PickUpItem(Pickup Item)
    {
        switch(Item.Type)
        {
            case Pickup.ItemType.Health:
                addHealth(Item.value);
                break;
            case Pickup.ItemType.Coin:
                addCoin(Item.value);
                break;
        }
    }

    public void addHealth(int value)
    {
        playerHealth.addHealth(value);
    }

    public void addCoin(int value)
    {
        Coin += value;
    }

    public void RotateToTarget()
    {
        if(currentState != CharacterState.Dead)
        {
            transform.LookAt(targetPlayer, Vector3.up);
        }
        
    }

    IEnumerator GameoverPanelTurn()
    {
        yield return new WaitForSeconds(1.2f);
        GameoverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
