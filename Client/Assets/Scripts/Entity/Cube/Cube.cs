using Extension;
using Network.GameServer;
using System;
using System.Collections;
using UnityEngine;

public class Cube : Entity
{
    public Action<Cube> OnShot;
    public Action<Cube, Vector3> OnMove;
    public Action<Cube, Cube> OnCombineMove;
    public Action<Cube, Cube> OnCombine;

    public Renderer range;
    private Animation anim;
    private Renderer render;

    public TextMesh combineLv_text;

    public float speed = 5.0f;
    private IEnumerator coroutineShot;

    public GameCube gameCube;
    private GameSlot gameSlot;
    public CubeDataXml.Data cubeData;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        render = GetComponentInChildren<Renderer>();

        range?.gameObject?.SetVisible(false);
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

    public void Spawn(GameCube gameCube, GameSlot gameSlot)
    {
        this.gameCube = gameCube;
        this.gameSlot = gameSlot;

        cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == gameSlot.CubeId);

        var color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2], 1f);
        render.material.color = color;
        range.material.color = new Color(color.r, color.g, color.b, 0.1f);
        range.transform.localScale = new Vector3(gameCube.CombineLv * 2f, gameCube.CombineLv * 2f, range.transform.localScale.z);
        combineLv_text.text = gameCube.CombineLv.ToString();

        base.Spawn();

        StartShot();
    }

    public void Selected()
    {
        range?.gameObject?.SetVisible(true);
    }

    public void DeSelected()
    {
        range?.gameObject?.SetVisible(false);
    }

    public void Move(Vector3 position)
    {
        var speed = 3f;

        base.Move(position, speed);

        anim.Play("Cube_Move");

        StopShot();
    }

    public void Combine(Cube cube)
    {
        if (gameCube.CombineLv != cube.gameCube.CombineLv)
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