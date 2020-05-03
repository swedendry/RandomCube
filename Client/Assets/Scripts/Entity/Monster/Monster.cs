using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    private enum State
    {
        Idle,
        Move,
        Die,
        Finish,
    }

    public Action<Monster> OnDie;
    public Action<Monster> OnEscape;

    public TextMesh hp_text;

    private List<Transform> paths;
    private float speed = 0.5f;
    private float hp = 100f;
    private State state = State.Idle;

    public int seq;

    public void Spawn(int seq)
    {
        this.seq = seq;
        hp = ServerDefine.MonsterSeq2HP(seq);
        speed = 1f;
        hp_text.text = ((int)hp).ToString();
        state = State.Idle;

        base.Spawn();
    }

    public void Move(List<GameObject> paths)
    {
        this.paths = paths.Select(x => x.transform).ToList();

        Move(1);
    }

    private void Move(int targetIndex)
    {
        var position = paths[targetIndex].position;
        position.z = 0f;
        var nextIndex = targetIndex + 1;

        state = State.Move;
        base.Move(position, speed, nextIndex);
    }

    protected override void MoveComplete(object cmpParams)
    {
        state = State.Idle;

        var targetIndex = int.Parse(cmpParams.ToString());
        if (paths.Count <= targetIndex)
        {   //Escape
            Escape();
            return;
        }

        Move(targetIndex);
    }

    private void Escape()
    {
        if (state != State.Idle)
            return;

        state = State.Finish;
        OnEscape?.Invoke(this);
    }

    public void Hit(Cube cube, Missile missile)
    {
        if (state != State.Move)
            return;

        Damage(cube.AD());
    }

    private void Damage(float damage)
    {
        EffectFactory.Spawn(EffectId.Damage, transform, 0f, (int)damage);

        hp -= damage;

        var virtualHP = Math.Max(1, hp);
        hp_text.text = ((int)virtualHP).ToString();

        if (hp <= 0)
        {
            state = State.Die;
            OnDie?.Invoke(this);
        }
    }
}
