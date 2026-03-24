using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                Debug.Log("Shooting at: " + hit.transform.name);
            }
        }

        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
    }
}
