using UnityEngine;

public class RespawnToPoint : MonoBehaviour
{
    public Transform respawnPoint;

    private bool hasBeenRewarded = false;

    public bool HasBeenRewarded()
    {
        return hasBeenRewarded;
    }

    public void MarkRewarded()
    {
        hasBeenRewarded = true;
    }

    public void ResetRewardState()
    {
        hasBeenRewarded = false;
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 👇 Reset reward state when teleported
        ResetRewardState();
    }
}