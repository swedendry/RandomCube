using UnityEngine;

public class Cube : Entity
{
    public float speed = 5.0f;
    private Vector3 target;
    private Animator anim;
    private bool isMove;

    private void Start()
    {
        target = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if(isMove && transform.position == target)
        {
            isMove = false;
            anim.SetBool("Move", false);
        }
    }

    public void Selected()
    {
        anim.SetBool("Selected", true);
    }

    public void DeSelected()
    {
        anim.SetBool("Selected", false);
    }

    public void Move(Vector3 target)
    {
        this.target = target;
        isMove = true;
        anim.SetBool("Move", true);
    }
}
