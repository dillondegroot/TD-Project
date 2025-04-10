using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    private void LateUpdate()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Pcam").GetComponent<Camera>(); 
        }

        if (cam != null)
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}
