using UnityEngine;

public enum RandomMode {
  Off,
  Unity,
  Seeded
}

[CreateAssetMenu(menuName =("IdleBattler / CombatSettings"))]
public class CombatSettings : ScriptableObject {
  public RandomMode randomMode = RandomMode.Unity;
  public int seed = 12345;

  [Header("Variance")]
  public PercentageRange movespeedVariance;
  public PercentageRange damageVariance;
}
