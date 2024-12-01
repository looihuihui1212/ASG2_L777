using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision with " + collision.transform.name);
        if (collision.transform.tag == "Ground")
        {
            Debug.Log("touch ground");
        }
    }

}
