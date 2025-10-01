// PlayerTriggerTester.cs
using UnityEngine;

public class PlayerTriggerTester : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        Debug.Log($"[PlayerTriggerTester] OnTriggerEnter with '{other.name}' (tag={other.tag})");
    }
    void OnCollisionEnter(Collision c) {
        Debug.Log($"[PlayerTriggerTester] OnCollisionEnter with '{c.collider.name}'");
    }
}
