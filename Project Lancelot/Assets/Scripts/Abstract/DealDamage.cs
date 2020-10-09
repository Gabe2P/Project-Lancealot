using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : Entity
{
    public Collider2D hitbox;
    public int damage = 1;

    private CameraManager cam;
    private void Start()
    {
        cam = CameraManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        Damagable enemy = collision.GetComponent<Damagable>();

        if (enemy != null)
        {
            cam.Shake();
            enemy.TakeDamage(damage);
        }
    }
}
