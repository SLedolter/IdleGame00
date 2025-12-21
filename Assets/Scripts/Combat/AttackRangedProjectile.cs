using UnityEngine;

public sealed class AttackRangedProjectile : AttackRangedBase {
  [Header("Projectile")]
  [SerializeField] private Projectile projectilePrefab;
  [SerializeField] private float projectileSpeed = 14f;

  protected override void FireAt(HumanBase target) {
    Debug.Log(self.name + " shot!");
    if (projectilePrefab == null) {
      Debug.LogWarning("Projectile prefab fehlt", this);
      return;
    }

    Vector3 origin = muzzle.position;
    Vector3 aimPoint = target.Health != null ? target.Health.Center.position : target.transform.position;

    Vector3 dir = (aimPoint - origin);
    dir.y = 0f;
    if (dir.sqrMagnitude < 0.0001f) { return; }
    dir.Normalize();

    var proj = Instantiate(projectilePrefab, origin, Quaternion.LookRotation(dir, Vector3.up));

    // Owner-Team für friendly fire
    var ownerTeam = self != null ? self.Team.Id : TeamId.TeamA;
    proj.Init(ownerTeam, damage, dir * projectileSpeed);
  }
}
