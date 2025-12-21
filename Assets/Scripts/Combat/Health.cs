using UnityEngine;

public interface IDamageable {
  Transform Center { get; }
  void TakeDamage(float amount);
  bool IsAlive { get; }
}

public sealed class Health : MonoBehaviour, IDamageable {
  [SerializeField] private float maxHp = 20f;
  [SerializeField] private Transform center; // Center-Empty

  public bool IsAlive => currentHp > 0f;
  public Transform Center => center != null ? center : transform;

  private float currentHp;

  private void Awake() {
    currentHp = maxHp;
  }

  public void TakeDamage(float amount) {
    if (!IsAlive) { return; }

    currentHp -= amount;
    if (currentHp <= 0f) {
      currentHp = 0f;
      // Für jetzt einfach deaktivieren, später Death-Animation, Loot, Pooling, etc.
      gameObject.SetActive(false);
    }
  }
}
