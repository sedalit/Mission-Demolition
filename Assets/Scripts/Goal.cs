using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool IsOver = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Goal.IsOver = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
