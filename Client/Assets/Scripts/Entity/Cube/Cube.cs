using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cube : Entity
{
    public Action<Cube> OnShot;
    public Action<Cube, Cube> OnCombine;

    public TextMesh grade_text;

    public float speed = 5.0f;
    private Animator anim;
    private IEnumerator coroutineShot;
    public float AP = 10f;
    public float AS = 1.5f;
    public int grade = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Spawn(int grade)
    {
        this.grade = grade;

        grade_text.text = grade.ToString();

        StartShot();

        base.Spawn();
    }

    public void Selected()
    {
        anim.SetBool("Selected", true);
    }

    public void DeSelected()
    {
        anim.SetBool("Selected", false);
    }

    public void Move(Vector3 position)
    {
        var speed = 3f;

        base.Move(position, speed);

        anim.SetBool("Move", true);
        StopShot();
    }

    public void Combine(Cube cube)
    {
        if (grade != cube.grade)
            return;

        var speed = 3f;

        Move(cube.transform.position, speed, "CombineComplete", cube);

        anim.SetBool("Move", true);
        StopShot();
    }

    protected override void MoveComplete(object cmpParams)
    {
        anim.SetBool("Move", false);

        coroutineShot = CoroutineShot();
        StartCoroutine(coroutineShot);
    }

    protected void CombineComplete(object cmpParams)
    {
        anim.SetBool("Move", false);

        OnCombine?.Invoke(this, (Cube)cmpParams);
    }

    public void Shot()
    {
        OnShot?.Invoke(this);
    }

    private void StopShot()
    {
        if (coroutineShot != null)
            StopCoroutine(coroutineShot);
    }

    private void StartShot()
    {
        StopShot();
        coroutineShot = CoroutineShot();
        StartCoroutine(coroutineShot);
    }

    private IEnumerator CoroutineShot()
    {
        Shot();

        var delay = 1f / AS;
        yield return new WaitForSeconds(1f / delay);

        StartShot();
    }

    public void Hit(Missile collider)
    {

    }
}
