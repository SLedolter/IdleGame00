using UnityEngine;

public interface IDamageable {
  Transform Center { get; }
  void TakeDamage(float amount);
  bool IsAlive { get; }
}