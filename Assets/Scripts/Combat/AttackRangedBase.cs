using UnityEngine;

public abstract class AttackRangedBase : AttackBase {
  [SerializeField] protected float damage = 1f;
  [SerializeField] protected float attacksPerSecond = 1f;

  [Header("Optional")]
  [SerializeField] protected Transform muzzle; // wenn null nehmen wir transform

  protected SimpleBrain brain;
  protected HumanBase self;

  private float nextAttackTime;

  protected virtual void Awake() {
    brain = GetComponent<SimpleBrain>();
    self = GetComponent<HumanBase>();

    if (muzzle == null) { muzzle = transform; }
  }

  protected virtual void Update() {
    var target = brain.CurrentTarget;
    if (target == null) { return; }
    if (Time.time < nextAttackTime) { return; }
    if (target.Health == null || !target.Health.IsAlive) { return; }

    // Distanzcheck (planar)
    Vector3 a = transform.position; a.y = 0f;
    Vector3 b = target.transform.position; b.y = 0f;
    if ((b - a).sqrMagnitude > Range * Range) { return; }

    FireAt(target);

    nextAttackTime = Time.time + (1f / Mathf.Max(0.01f, attacksPerSecond));
  }

  protected abstract void FireAt(HumanBase target);
}
