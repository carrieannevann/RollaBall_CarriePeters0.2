// PickupItem.cs
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound;
    public bool destroyOnPickup = true;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[PickupItem] OnTriggerEnter: other='{other.name}', tag='{other.tag}' on pickup '{name}'.");

        if (!other.CompareTag(playerTag))
        {
            Debug.Log("[PickupItem] Trigger ignore: not player.");
            return;
        }

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        if (UIManager.Instance != null)
        {
            Debug.Log("[PickupItem] Calling UIManager.Instance.OnPickupCollected()");
            UIManager.Instance.OnPickupCollected();
        }
        else
        {
            Debug.LogWarning("[PickupItem] UIManager.Instance not found in scene.");
        }

        if (destroyOnPickup)
            Destroy(gameObject);
    }
}
