using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isTriggered = false;

    // Called when another collider enters this object's trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScanPoint"))
        {
            isTriggered = true;
        }
    }

    // Called when another collider exits this object's trigger zone.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ScanPoint"))
        {
            isTriggered = false;
        }
    }
}