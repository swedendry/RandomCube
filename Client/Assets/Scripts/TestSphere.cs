using UnityEngine;

public class TestSphere : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter TestSphere");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit TestSphere");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter TestSphere");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay TestSphere");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit TestSphere");
    }
}
