using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    public RewardManager rewardManager;

    private Dictionary<Collider, Coroutine> activeCoroutines = new Dictionary<Collider, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RespawnToPoint respawn))
        {
            // Don't start if already rewarded this cycle
            if (respawn.HasBeenRewarded()) return;

            // Don't stack coroutines
            if (!activeCoroutines.ContainsKey(other))
            {
                Coroutine coroutine = StartCoroutine(WaitAndRespawn(respawn, other));
                activeCoroutines.Add(other, coroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (activeCoroutines.ContainsKey(other))
        {
            StopCoroutine(activeCoroutines[other]);
            activeCoroutines.Remove(other);
        }
    }

    private IEnumerator WaitAndRespawn(RespawnToPoint respawn, Collider other)
    {
        yield return new WaitForSeconds(2f);

        if (IsStillOverlapping(other))
        {
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null && rb.linearVelocity.magnitude < 0.1f && rb.angularVelocity.magnitude < 0.1f)
            {
                // ✅ Reward only if it hasn't been rewarded this cycle
                if (!respawn.HasBeenRewarded())
                {
                    rewardManager.AddReward(1);
                    respawn.MarkRewarded();  // Lock until next respawn
                    respawn.Respawn();       // Resets state inside Respawn()
                }
            }
        }

        activeCoroutines.Remove(other);
    }

    private bool IsStillOverlapping(Collider other)
    {
        Collider[] overlapping = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation);
        foreach (Collider col in overlapping)
        {
            if (col == other)
                return true;
        }
        return false;
    }
}