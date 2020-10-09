using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damagable
{
    int CurrentHealth();

    int AddHealth(int amount);

    int TakeDamage(int amount);
}
