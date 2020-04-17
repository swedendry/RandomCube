using UnityEngine;

public class Cube : Entity
{
    public float speed = 5.0f;
    private Vector3 target;
    private Animation anim;

    private void Start()
    {
        target = transform.position;
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void Selected()
    {
        anim.Play("Cube_Selected");
    }

    public void DeSelected()
    {
        anim.Stop();
    }

    public void Move(Vector3 target)
    {
        this.target = target;
    }
}
