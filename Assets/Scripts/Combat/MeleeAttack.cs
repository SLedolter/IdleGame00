using UnityEngine;

[DisallowMultipleComponent]
public sealed class MeleeAttack : MonoBehaviour {
  [SerializeField] private float range = 1.2f;
  [SerializeField] private float damage = 1f;
  [SerializeField] private float attacksPerSecond = 1f;

  private SimpleBrain brain;
  private float nextAttackTime;

  private void Awake() {
    brain = GetComponent<SimpleBrain>();
    if (brain == null) { Debug.LogError("Melee-Attack braucht SimpleBrain-Komponente", this); }
  }

  private void Update() {
    var target = brain.CurrentTarget;
    if(target == null) { return; }
    if (Time.time < nextAttackTime) { return; }

    Vector3 a = transform.position; a.y = 0f;
    Vector3 b = target.transform.position; b.y = 0f;

    if((b - a).sqrMagnitude > range * range) { return; }

    // Schaden
    target.Health.TakeDamage(damage);
    Debug.Log(brain.self.name + " deals " + damage);
    if(!target.Health.IsAlive) {
      Debug.Log(brain.self.name + " wins!");
    }
    nextAttackTime = Time.time + (1f / Mathf.Max(0.01f, attacksPerSecond));
  }

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
