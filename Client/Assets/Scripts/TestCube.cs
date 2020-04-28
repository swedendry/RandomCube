using UnityEngine;

public class TestCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter TestCube");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit TestCube");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter TestCube");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay TestCube");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit TestCube");
    }
}
