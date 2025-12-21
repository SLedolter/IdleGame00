using UnityEngine;
public abstract class AttackBase : MonoBehaviour, IAttack {
  [SerializeField] protected float range = 1.2f;
  public float Range => range;
}
