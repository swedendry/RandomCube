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
        hp = 100f;
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

        state = State.Finish;
        iTween.Stop(gameObject);
        DeleteSkills();
        OnEscape?.Invoke(this);
    }

    public void Hit(Cube cube, Missile collider)
    {
        if (state != State.Move)
            return;

        hp -= cube.AD();
        hp_text.text = ((int)hp).ToString();

        var skillKey = "Skill_Ice";
        if (skills.ContainsKey(skillKey))
        {   //같은 스킬 활성화중
            DeleteSkill(skillKey);
        }

        StartCoroutine(Particle("Skill_Ice"));

        if (hp <= 0)
        {
            state = State.Die;
            iTween.Stop(gameObject);
            DeleteSkills();
            OnDie?.Invoke(this, collider);
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
