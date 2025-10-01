// UIManager.cs
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI pickupTextTMP;   // assign PickupCounterText here

    [Header("Optional Settings")]
    public bool autoCountPickupsAtStart = true;

    int remainingPickups = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (autoCountPickupsAtStart)
        {
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
            remainingPickups = pickups != null ? pickups.Length : 0;
            Debug.Log($"[UIManager] Found {remainingPickups} objects tagged 'PickUp' at Start.");
        }
        UpdateUI();
    }

    public void OnPickupCollected()
    {
        Debug.Log("[UIManager] OnPickupCollected() called. remaining before = " + remainingPickups);
        remainingPickups = Mathf.Max(0, remainingPickups - 1);
        UpdateUI();
        Debug.Log("[UIManager] remaining after = " + remainingPickups);
        if (remainingPickups == 0) LevelComplete();
    }

    public void AddPickup(int amount = 1)
    {
        remainingPickups += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (pickupTextTMP != null)
        {
            pickupTextTMP.text = $"PickUps: {remainingPickups}";
        }
        else
        {
            Debug.LogWarning("[UIManager] pickupTextTMP not assigned in Inspector!");
        }
    }

    void LevelComplete()
    {
        Debug.Log("[UIManager] All pickups collected!");
    }

    // small debug helper: press K to simulate a pickup in play mode
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) OnPickupCollected();
    }
}
