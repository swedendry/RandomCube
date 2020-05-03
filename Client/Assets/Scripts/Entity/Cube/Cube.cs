using Extension;
using Network.GameServer;
using System;
using System.Collections;
using UnityEngine;

public class Cube : Entity
{
    private enum State
    {
        Idle,
        Move,
        CombineMove,
    }

    public Action<Cube> OnShot;
    public Action<Cube, Vector3> OnMove;
    public Action<Cube, Cube> OnCombineMove;
    public Action<Cube, Cube> OnCombine;

    public Renderer range;
    private Animation anim;
    private Renderer render;

    private State state = State.Idle;
    public TextMesh combineLv_text;

    public float speed = 5.0f;
    private IEnumerator coroutineShot;

    public string ownerId;
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
        return ServerDefine.SlotLv2AD(cubeData.AD, gameSlot.CubeLv, gameSlot.SlotLv);
    }

    public float AS()
    {
        return ServerDefine.SlotLv2AS(cubeData.AS, gameSlot.CubeLv, gameSlot.SlotLv);
    }

    public void Spawn(string userId, GameCube gameCube, GameSlot gameSlot)
    {
        ownerId = userId;
        this.gameCube = gameCube;
        this.gameSlot = gameSlot;
        state = State.Idle;

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

        state = State.Move;
        base.Move(position, speed);

        anim.Play("Cube_Move");

        StopShot();
    }

    public void CombineMove(Cube target)
    {
        if (gameCube.CubeSeq == target.gameCube.CubeSeq)
            return;

        if (gameCube.CubeId != target.gameCube.CubeId)
            return;

        if (gameCube.CombineLv != target.gameCube.CombineLv)
            return;

        if (state == State.CombineMove)
            return;

        OnCombineMove?.Invoke(this, target);
    }

    public void Combine(Cube cube)
    {
        var speed = 3f;

        state = State.CombineMove;
        Move(cube.transform.position, speed, "CombineComplete", cube);

        anim.Play("Cube_Move");
        StopShot();
    }

    protected override void MoveComplete(object cmpParams)
    {
        state = State.Idle;

        anim.Stop("Cube_Move");
        anim.transform.localRotation = Quaternion.identity;

        coroutineShot = CoroutineShot();
        StartCoroutine(coroutineShot);
    }

    protected void CombineComplete(object cmpParams)
    {
        state = State.Idle;

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

    public void Shot(Missile missile)
    {
        anim.Play("Cube_Shot");
    }

    public void Hit(Missile missile)
    {

    }
}