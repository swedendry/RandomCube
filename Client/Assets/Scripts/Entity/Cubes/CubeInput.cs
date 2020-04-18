﻿using UnityEngine;

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
                var cube = hit.collider.GetComponent<Cube>();
                if (cube)
                {   //cube
                    if(selectedCube)
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
                        selectedCube.Move(hit.point);
                    }
                }
            }
        }

    }
}