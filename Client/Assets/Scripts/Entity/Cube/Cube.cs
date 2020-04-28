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

    public TextMesh combineLv_text;

    [NonSerialized]
    public byte combineLv = 1;

    public float speed = 5.0f;
    private IEnumerator coroutineShot;

    private GameSlot gameSlot;
    public CubeDataXml.Data cubeData;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        render = GetComponentInChildren<Renderer>();

        range.gameObject.SetActive(false);
    }

    public float AD()
    {
        var slotLv = gameSlot.SlotLv;

        return cubeData.AD * slotLv;
    }

    public float AS()
    {
        var slotLv = gameSlot.SlotLv;

        return cubeData.AS / slotLv;
    }

    public void Spawn(byte combineLv, GameSlot gameSlot)
    {
        this.combineLv = combineLv;
        this.gameSlot = gameSlot;

        cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == gameSlot.CubeId);

        var color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2], 1f);
        render.material.color = color;
        range.material.color = new Color(color.r, color.g, color.b, 0.2f);

        combineLv_text.text = combineLv.ToString();

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
        if (combineLv != cube.combineLv)
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
        Shot();

        yield return new WaitForSeconds(AS());

        StartShot();
    }

    public void Hit(Missile collider)
    {

    }
}


//var color = new Color(0f, 0f, 0f);
//        switch (gameSlot.CubeId)
//        {
//            case 1:
//                {
//                    color.r = 1f; color.g = 0f; color.b = 0f;
//                }
//                break;
//            case 2:
//                {
//                    color.r = 1f; color.g = 0.4f; color.b = 0f;
//                }
//                break;
//            case 3:
//                {
//                    color.r = 1f; color.g = 1f; color.b = 0f;
//                }
//                break;
//            case 4:
//                {
//                    color.r = 0f; color.g = 1f; color.b = 0f;
//                }
//                break;
//            case 5:
//                {
//                    color.r = 0f; color.g = 0f; color.b = 1f;
//                }
//                break;
//            case 6:
//                {
//                    color.r = 0f; color.g = 0.4f; color.b = 1f;
//                }
//                break;
//            case 7:
//                {
//                    color.r = 1f; color.g = 0f; color.b = 1f;
//                }
//                break;
//            default:
//                break;
//        }