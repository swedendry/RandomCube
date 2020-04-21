using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    private enum State
    {
        Spawn,
        Move,
        Die,
        Finish,
    }

    public Action<Monster, Missile> OnDie;
    public Action<Monster> OnFinish;

    public TextMesh hp_text;

    private List<Transform> paths;
    private int targetIndex = 0;
    private float speed = 1f;
    private float hp = 400f;
    private State state = State.Spawn;

    public override void Spawn()
    {
        iTween.Stop(gameObject);
        hp = 400f;
        speed = 1f;
        hp_text.text = ((int)hp).ToString();
        state = State.Spawn;

        base.Spawn();
    }

    public void Move(List<GameObject> paths)
    {
        state = State.Move;

        this.paths = paths.Select(x => x.transform).ToList();

        Move(1);
    }

    public void Move(int targetIndex)
    {
        this.targetIndex = targetIndex;

        var position = paths[targetIndex].position;
        position.z = 0f;
        var nextIndex = targetIndex + 1;

        base.Move(position, speed, nextIndex);
    }

    protected override void MoveComplete(object cmpParams)
    {
        var targetIndex = int.Parse(cmpParams.ToString());
        if (paths.Count <= targetIndex)
        {   //Finish
            Finish();
            return;
        }

        Move(targetIndex);
    }

    private void Finish()
    {
        if (state != State.Move)
            return;

        state = State.Finish;
        iTween.Stop(gameObject);
        OnFinish?.Invoke(this);
    }

    public void Hit(Cube cube, Missile collider)
    {
        if (state != State.Move)
            return;

        hp -= cube.AP;
        hp_text.text = ((int)hp).ToString();

        StartCoroutine(Particle());

        if (hp <= 0)
        {
            state = State.Die;
            iTween.Stop(gameObject);
            OnDie?.Invoke(this, collider);
        }
    }

    private IEnumerator Particle()
    {
        var key = "Skill_Ice";
        var skill = PoolFactory.Get(key);
        skill.transform.parent = transform;
        skill.transform.localPosition = Vector3.zero;
        skill.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        PoolFactory.Return(key, skill);
    }
}
