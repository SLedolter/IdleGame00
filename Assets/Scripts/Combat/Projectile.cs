using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Projectile : MonoBehaviour {
  [SerializeField] private float lifetime = 3f;
  [SerializeField] private LayerMask hitMask = ~0;

  private Rigidbody rb;
  private TeamId ownerTeam;
  private float damage;

  private void Awake() {
    rb = GetComponent<Rigidbody>();
    rb.useGravity = false;
    rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    rb.interpolation = RigidbodyInterpolation.Interpolate;
  }

  public void Init(TeamId ownerTeam, float damage, Vector3 velocity) {
    this.ownerTeam = ownerTeam;
    this.damage = damage;

    rb.linearVelocity = velocity;
    Destroy(gameObject, lifetime);
  }

  private void OnTriggerEnter(Collider other) {
    // Layermask Filter (optional, aber oft sinnvoll)
    if (((1 << other.gameObject.layer) & hitMask.value) == 0) { return; }

    var hitFighter = other.GetComponentInParent<HumanBase>();
    if (hitFighter != null && hitFighter.Team != null && hitFighter.Team.Id == ownerTeam) { return; }

    var dmg = other.GetComponentInParent<IDamageable>();
    dmg?.TakeDamage(damage);

    Destroy(gameObject);
  }
}
