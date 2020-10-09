using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : Entity, Damagable
{
    public float moveSpeed = 1f;
    private float speed;
    public Vector2 movement = Vector2.zero;

    private bool facingRight = true;

    private bool isRolling = false;

    private Rigidbody2D motor;

    private CapsuleCollider2D hitbox;

    private AudioManager sfx;
    private CameraManager cam;

    public string rollingSFX = "Roll";
    public string attackSFX = "Attack";
    public string walkingSFX = "Walk";
    private float curTime = 0f;

    public Animator anim;

    public GameObject attack;
    public Transform attackPos;

    public float attackCoolDown = .25f;
    private float curCoolDown = 0f;
    public float attackStutter = .25f;
    private float curStutter = 0;

    public float rollMultiplier = 1.5f;
    public float maxRoll = .5f;
    private float curRoll = 0f;

    public int maxHealth = 5;
    private int curHealth = 0;

    private void Awake()
    {
        curHealth = maxHealth;
        speed = moveSpeed;
        motor = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        
    }


    // Start is called before the first frame update
    void Start()
    {
        sfx = AudioManager.instance;
        cam = CameraManager.instance;
    }

    public int CurrentHealth()
    {
        return this.curHealth;
    }

    public int AddHealth(int amount)
    {
        return this.curHealth;
    }

    public int TakeDamage(int amount)
    {
        return this.curHealth;
    }

    public void Move(Vector2 moveInput)
    {
        if (!isRolling && curStutter <= 0)
        {
            if (((moveInput.x > 0 && !facingRight) || (moveInput.x < 0 && facingRight)))
            {
                Flip();
            }

            if ((moveInput.x > .5f || moveInput.x < -.5f) || (moveInput.y > .5f) || moveInput.y < -.5f)
            {
                movement = moveInput.normalized * speed;
            }
            else
            {
                movement = Vector2.zero;
            }
        }
    }

    public void Special()
    {
        if (!isRolling)
        {
            anim.SetTrigger("Roll");
            sfx.Play(rollingSFX);
            isRolling = true;
            hitbox.enabled = false;
        }
    }

    public void Action()
    {
        if (curCoolDown >= attackCoolDown && !isRolling)
        {
            curStutter = attackStutter;
            anim.SetTrigger("Attack");
            sfx.Play(attackSFX);
            this.movement = Vector2.zero;
            GameObject clone = Instantiate(attack, attackPos.position, Quaternion.identity);
            clone.transform.rotation = this.transform.rotation;
            Destroy(clone, .15f);
            curCoolDown = 0;
        }
    }

    private void Update()
    {
        if (curCoolDown < attackCoolDown)
        {
            curCoolDown += Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        if (isRolling)
        {
            if (curRoll < maxRoll)
            {
                if (movement != Vector2.zero)
                {
                    motor.MovePosition(motor.position + movement * rollMultiplier * Time.fixedDeltaTime);
                }
                else
                {
                    if (facingRight)
                    {
                        movement.x = 1;
                        motor.MovePosition(motor.position + movement * rollMultiplier * Time.fixedDeltaTime);
                    }
                    else
                    {
                        movement.x = -1;
                        motor.MovePosition(motor.position + movement * rollMultiplier * Time.fixedDeltaTime);
                    }
                }
                curRoll += Time.deltaTime;
            }
            else
            {
                isRolling = false;
                hitbox.enabled = true;
                curRoll = 0;
            }
        }
        else
        {
            if (curStutter > 0)
            {
                curStutter -= Time.fixedDeltaTime;
            }
            else
            {
                if (movement != Vector2.zero)
                {
                    anim.SetBool("Moving", true);
                }
                else
                {
                    anim.SetBool("Moving", false);
                }

                motor.MovePosition(motor.position + movement * Time.fixedDeltaTime);

                if (movement != Vector2.zero)
                {
                    if (curTime <= sfx.GetSoundLength(walkingSFX)/1.5)
                    {
                        curTime += Time.fixedDeltaTime;
                    }
                    else
                    {
                        sfx.Play(walkingSFX);
                        curTime = 0f;
                    }
                }
            }

        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        this.transform.Rotate(0f, 180f, 0f);
    }



}
