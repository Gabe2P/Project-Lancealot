using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    private AudioManager sfx;

    public string rollingSFX = "Roll";
    public string attackSFX = "Attack";
    public string walkingSFX = "Walk";

    public float attackSpeed;
    private float curTime = 0f;

    public Animator anim;

    public GameObject attack;
    public Transform attackPos;

    public override void Attack()
    {
        anim.SetTrigger("Attack");
        sfx.Play(attackSFX);
        GameObject clone = Instantiate(attack, attackPos.position, Quaternion.identity);
        clone.transform.rotation = this.transform.rotation;
        Destroy(clone, .15f);
    }

    public override void Move()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(this.transform.position, target.transform.position);

            if (distance > nav.stoppingDistance + this.GetCurrentAttackRange())
            {
                nav.SetDestination(target.transform.position);
            }
        }
    }

    public void Dodge()
    {

    }

    public override void Routine()
    {
        Move();

        float distance = Vector2.Distance(this.transform.position, target.transform.position);
        
        if (distance <= nav.stoppingDistance + this.GetCurrentAttackRange())
        {
            if (curTime < attackSpeed)
            {
                curTime += Time.fixedDeltaTime;
            }
            else
            {
                Attack();
                curTime = 0f;
            }
        }
    }
}
