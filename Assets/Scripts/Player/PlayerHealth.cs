using System;
using Entities;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Destroy(this.gameObject);
    }
}
