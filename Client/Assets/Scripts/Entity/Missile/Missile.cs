using System;
using UnityEngine;

public class Missile : Entity
{
    public Action<Cube, Monster, Missile> OnHit;

    private Cube owner;
    private Monster target;
    private readonly float speed = 3f;
    private bool shoting = false;

    private void Update()
    {
        if (!shoting || target == null)
            return;

        var targetPosition = target.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {   //명중
            Hit();
        }
    }

    public void Shot(Cube owner, Monster target)
    {
        shoting = true;

        this.owner = owner;
        this.target = target;
    }

    private void Hit()
    {
        shoting = false;

        OnHit?.Invoke(owner, target, this);
    }
}
