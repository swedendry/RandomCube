using Network.GameServer;
using System;
using System.Collections;
using UnityEngine;

public class Cube : Entity
{
    public Action<Cube> OnShot;
    public Action<Cube, Cube> OnCombine;

    public Renderer range;
    private Animation anim;
    private Renderer render;

    public TextMesh grade_text;

    public float speed = 5.0f;
    private IEnumerator coroutineShot;
    [NonSerialized]
    public float AP = 50f;
    [NonSerialized]
    public float AS = 0.1f;
    [NonSerialized]
    public int grade = 1;
    private GameSlot gameSlot;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        render = GetComponentInChildren<Renderer>();

        range.gameObject.SetActive(false);
    }

    public void Spawn(int grade, GameSlot gameSlot)
    {
        this.grade = grade;
        this.gameSlot = gameSlot;

        var color = new Color(0f, 0f, 0f);
        switch (gameSlot.CubeId)
        {
            case 1:
                {
                    color.r = 1f; color.g = 0f; color.b = 0f;
                }
                break;
            case 2:
                {
                    color.r = 1f; color.g = 0.4f; color.b = 0f;
                }
                break;
            case 3:
                {
                    color.r = 1f; color.g = 1f; color.b = 0f;
                }
                break;
            case 4:
                {
                    color.r = 0f; color.g = 1f; color.b = 0f;
                }
                break;
            case 5:
                {
                    color.r = 0f; color.g = 0f; color.b = 1f;
                }
                break;
            case 6:
                {
                    color.r = 0f; color.g = 0.4f; color.b = 1f;
                }
                break;
            case 7:
                {
                    color.r = 1f; color.g = 0f; color.b = 1f;
                }
                break;
            default:
                break;
        }
        render.material.color = color;
        range.material.color = new Color(color.r, color.g, color.b, 0.2f);

        grade_text.text = grade.ToString();

        StartShot();

        base.Spawn();
    }

    public void Selected()
    {
        //anim.Play("Cube_Selected");

        range.gameObject.SetActive(true);
        //anim.SetBool("Selected", true);
    }

    public void DeSelected()
    {
        //anim.Stop("Cube_Selected");

        range.gameObject.SetActive(false);
        //anim.SetBool("Selected", false);
    }

    public void Move(Vector3 position)
    {
        var speed = 3f;

        base.Move(position, speed);

        anim.Play("Cube_Move");
        //anim.SetBool("Move", true);
        StopShot();
    }

    public void Combine(Cube cube)
    {
        if (grade != cube.grade)
            return;

        var speed = 3f;

        Move(cube.transform.position, speed, "CombineComplete", cube);

        anim.Play("Cube_Move");
        StopShot();
    }

    protected override void MoveComplete(object cmpParams)
    {
        anim.Stop("Cube_Move");
        anim.transform.localRotation = Quaternion.identity;

        coroutineShot = CoroutineShot();
        StartCoroutine(coroutineShot);
    }

    protected void CombineComplete(object cmpParams)
    {
        anim.Stop("Cube_Move");
        anim.transform.localRotation = Quaternion.identity;

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
        Debug.Log("Shot : " + AS);

        Shot();

        yield return new WaitForSeconds(AS);

        StartShot();
    }

    public void Hit(Missile collider)
    {

    }
}
