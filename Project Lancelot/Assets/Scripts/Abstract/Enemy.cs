using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, Damagable
{
    [Header("Spawn Settings")]

    [Tooltip("The percentage chance this will be spawned if picked to be spawned.")] [Range(0f, 1f)] public float spawnChance;
    [Tooltip("The minimum distance from the players spawn position before this can spawn.")] [Min(0f)] public float minimumSpawnDistance;

    public float moveSpeed = .5f;
    private float speed = 0f;

    public int maxHealth = 5;
    private int curHealth = 0;

    public float attackRange = 1f;
    private float curAttackRange = 0f;

    public GameObject target;

    private bool facingRight = false;

    [HideInInspector]public NavMeshAgent2D nav;
    [HideInInspector] public BoxCollider2D hitbox;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        curHealth = maxHealth;
        nav = GetComponent<NavMeshAgent2D>();
        hitbox = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        curAttackRange = attackRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.AddEnemy(this);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
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

    virtual public void Attack()
    {

    }

    virtual public void Move()
    {

    }

    virtual public void Routine()
    {

    }



    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Routine();
    }

    public float GetCurrentAttackRange()
    {
        return this.curAttackRange;
    }

    public void SetCurrentAttackRange(float amount)
    {
        curAttackRange = amount;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        this.transform.Rotate(0f, 180f, 0f);
    }
}
