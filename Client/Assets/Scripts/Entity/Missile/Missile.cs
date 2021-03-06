﻿using System;
using UnityEngine;

public class Missile : Entity
{
    public Action<Cube, Monster, Missile> OnHit;

    private Cube owner;
    private Monster target;
    private readonly float speed = 10f;
    private bool shoting = false;

    private Renderer render;

    private void Awake()
    {
        render = GetComponentInChildren<Renderer>();
    }

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

    public void Spawn(Cube owner, Monster target)
    {
        shoting = true;

        this.owner = owner;
        this.target = target;

        var cubeData = owner.cubeData;
        render.material.color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2], 1f);

        base.Spawn();
    }

    private void Hit()
    {
        shoting = false;

        OnHit?.Invoke(owner, target, this);
    }
}
