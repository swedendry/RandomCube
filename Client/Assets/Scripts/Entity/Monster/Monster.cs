using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    public Action<Monster, Missile> OnDie;
    public Action<Monster> OnFinish;

    public TextMesh hp_text;
    private List<Transform> paths;
    private int targetIndex = 0;
    private float speed = 1f;
    private float hp = 400f;

    public override void Spawn()
    {
        iTween.Stop(gameObject);
        hp = 400f;
        speed = 1f;
        hp_text.text = ((int)hp).ToString();

        base.Spawn();
    }

    public void Move(List<GameObject> paths)
    {
        this.paths = paths.Select(x => x.transform).ToList();

        Move(1);
    }

    public void Move(int targetIndex)
    {
        this.targetIndex = targetIndex;

        var position = paths[targetIndex].position;
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
        iTween.Stop(gameObject);
        OnFinish?.Invoke(this);
    }

    public void Hit(Cube cube, Missile collider)
    {
        hp -= cube.AP;
        hp_text.text = ((int)hp).ToString();

        if(hp <= 0)
        {
            iTween.Stop(gameObject);
            OnDie?.Invoke(this, collider);
        }
    }
}
