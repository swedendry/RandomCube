using UnityEngine;

public class CubeInput : MonoBehaviour
{
    private Cube selectedCube;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var layerMask = LayerMask.GetMask("Box", "Cube");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10000f, layerMask))
            {
                var cube = hit.collider.GetComponentInParent<Cube>();
                if (cube)
                {   //cube
                    if (selectedCube)
                    {
                        selectedCube.DeSelected();
                    }

                    selectedCube = cube;
                    selectedCube.Selected();
                }
                else
                {   //box
                    if (selectedCube)
                    {
                        selectedCube.OnMove?.Invoke(selectedCube, hit.point);//.Move(hit.point);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (selectedCube)
            {
                var layerMask = LayerMask.GetMask("Cube");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 10000f, layerMask))
                {
                    var cube = hit.collider.GetComponentInParent<Cube>();
                    if (cube)
                    {
                        selectedCube.Combine(cube);
                    }
                }
            }
        }
    }
}
