using Extension;
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
    public Action<Monster> OnEscape;

    public TextMesh hp_text;

    private List<Transform> paths;
    private float speed = 0.5f;
    private float hp = 100f;
    private State state = State.Spawn;
    private readonly Dictionary<string, GameObject> skills = new Dictionary<string, GameObject>();

    public int seq;

    public void Spawn(int seq)
    {
        this.seq = seq;
        iTween.Stop(gameObject);
        hp = ServerDefine.MonsterSeq2HP(seq);
        speed = 1f;
        hp_text.text = ((int)hp).ToString();
        state = State.Spawn;

        base.Spawn();
    }

    public override void Release()
    {
        iTween.Stop(gameObject);
        DeleteSkills();
    }

    public void Move(List<GameObject> paths)
    {
        state = State.Move;

        this.paths = paths.Select(x => x.transform).ToList();

        Move(1);
    }

    private void Move(int targetIndex)
    {
        var position = paths[targetIndex].position;
        position.z = 0f;
        var nextIndex = targetIndex + 1;

        base.Move(position, speed, nextIndex);
    }

    protected override void MoveComplete(object cmpParams)
    {
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
        if (state != State.Move)
            return;

        Debug.LogWarning(string.Format("Escape {0}:{1}", transform.parent.name, seq));

        state = State.Finish;
        OnEscape?.Invoke(this);
    }

    public void Hit(Cube cube, Missile missile)
    {
        if (state != State.Move)
            return;

        hp -= cube.AD();

        var virtualHP = Math.Max(1, hp);
        hp_text.text = ((int)virtualHP).ToString();

        //var skillKey = "Skill_Ice";
        //if (skills.ContainsKey(skillKey))
        //{   //같은 스킬 활성화중
        //    DeleteSkill(skillKey);
        //}

        //StartCoroutine(Particle("Skill_Ice"));

        if (hp <= 0)
        {
            Debug.LogWarning(string.Format("Die {0}:{1}", transform.parent.name, seq));

            state = State.Die;
            OnDie?.Invoke(this, missile);
        }
    }

    private void DeleteSkills()
    {
        foreach (var skill in skills)
        {
            StopCoroutine(Particle(skill.Key));
            PoolFactory.Return(skill.Key, skill.Value);
        }

        skills.Clear();
    }

    private void DeleteSkill(string key)
    {
        if (!skills.ContainsKey(key))
            return;

        var skill = skills[key];
        StopCoroutine(Particle(key));
        PoolFactory.Return(key, skill);
        skills.Remove(key);
    }

    private IEnumerator Particle(string key)
    {
        var skill = PoolFactory.Get(key, transform);
        var particle = skill.GetComponent<ParticleSystem>();
        skill.transform.localPosition = new Vector3(0f, 0f, 0f);
        skill?.gameObject?.SetVisible(true);
        skills.Add(key, skill);

        yield return new WaitForSeconds(2f);

        DeleteSkill(key);
    }
}
