using UnityEngine;

public class TriggerDisappear : MonoBehaviour
{
    public GameObject objectToDisappear; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Make the target object disappear
            if (objectToDisappear != null)
            {
                objectToDisappear.SetActive(false);
            }
        }
    }
}
