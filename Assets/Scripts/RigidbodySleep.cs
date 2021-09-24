using UnityEngine;

public class RigidbodySleep : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        if (rigid != null) rigid.Sleep();
    }
}
