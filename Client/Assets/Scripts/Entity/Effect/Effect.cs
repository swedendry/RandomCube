using System;
using System.Collections;
using UnityEngine;

public class Effect : Entity
{
    public Action<string, GameObject> OnReturn;

    protected string key;
    private Transform target;

    protected void Spawn(string key, Transform target)
    {
        this.key = key;
        this.target = target;

        transform.position = target.position;

        base.Spawn();
    }

    public virtual void Spawn(string key, Transform target, float durationTime, params object[] values)
    {
        Spawn(key, target);

        StartCoroutine(CoroutineTimer(durationTime));
    }

    protected void Return()
    {
        OnReturn?.Invoke(key, gameObject);
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
            return;

        transform.position = target.position;
    }

    protected IEnumerator CoroutineTimer(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        Return();
    }
}
