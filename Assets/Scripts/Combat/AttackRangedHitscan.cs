using UnityEngine;

public class AttackRangedScan : AttackRangedBase {
  [Header("Hitscan")]
  [SerializeField] private LayerMask hitMask = ~0;
  [SerializeField] private float maxDistance = 50f;

  protected override void FireAt(HumanBase target) {
    Vector3 origin = muzzle.position;

    // Zielpunkt: Center, falls vorhanden (sauberer als transform)
    Vector3 aimPoint = target.Health != null ? target.Health.Center.position : target.transform.position;

    Vector3 dir = (aimPoint - origin);
    dir.y = 0f;
    if(dir.sqrMagnitude < 0.0001f) { return; }
    dir.Normalize();

    // Raycast
    if(Physics.Raycast(origin, dir, out RaycastHit hit, maxDistance, hitMask, QueryTriggerInteraction.Ignore)) {
      // Wir treffen evtl. Collider-Childs -> IDamageable im parent suchen
      var dmg = hit.collider.GetComponentInParent<IDamageable>();
      var hitFighter = hit.collider.GetComponentInParent<HumanBase>();

      // optional: friendly fire verhindern
      if(hitFighter != null && self != null && hitFighter.Team != null && self.Team != null) {
        if(hitFighter.Team.Id == self.Team.Id) { return; }

        dmg?.TakeDamage(damage);

        Debug.DrawLine(origin, hit.point, Color.red, 0.1f);
      }
    }
  }
}
