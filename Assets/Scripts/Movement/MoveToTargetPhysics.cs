using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public sealed class MoveToTargetPhysics : MonoBehaviour {
  [SerializeField] private float moveSpeed = 2.5f;
  [SerializeField] private float stoppingDistance = 1.1f;

  private SimpleBrain brain;
  private Rigidbody rb;

  private void Awake() {
    brain = GetComponent<SimpleBrain>();
    if (brain == null) { Debug.LogError("MoveToTarget braucht Simple-Brain-Komponente"); }

    rb = GetComponent<Rigidbody>();
  }

  private void FixedUpdate() {
    var target = brain.CurrentTarget;
    if (target == null) { return; }

    Vector3 toTarget = target.transform.position - rb.position;
    toTarget.y = 0f;

    float dist = toTarget.magnitude;
    if (dist < stoppingDistance) {
      // sanftes bremsen
      rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
      return;
    }

    Vector3 dir = toTarget / Mathf.Max(0.0001f, dist);
    Vector3 desired = dir * moveSpeed;

    // Rigidbody "steuern"
    rb.linearVelocity = new Vector3(desired.x, rb.linearVelocity.y, desired.z);

    // optional: ansehen
    if (dir.sqrMagnitude > 0.0001f) {
      Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
      rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 12f * Time.fixedDeltaTime));
    }
  }
}