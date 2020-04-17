using UnityEngine;

public class MyCube : Cube
{
    public float m_fSpeed = 5.0f;
    private Vector3 m_vecTarget;

    private void Start()
    {
        m_vecTarget = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var layerMask = LayerMask.GetMask("Box", "Cube");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10000f, layerMask))
            {
                var hihi = hit.collider.GetComponent<Cube>();

                Debug.Log(hit.collider.name);
                m_vecTarget = hit.point;
                m_vecTarget.z = 0f;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, m_vecTarget, m_fSpeed * Time.deltaTime);
    }
}
