using UnityEngine;

public struct PercentageRange {
  [Tooltip("z.B. -0.10 für -10%")]
  public float min;
  [Tooltip("z.B. 0.05 für 5%")]
  public float max;

  public float SampleMultiplier(IRandomSource rng) {
    float pct = rng.Range(min, max);
    return 1f + pct;
  }
}

public static class Variance {
  public static float ApplyMultiplier(float baseValue, float multiplier) => baseValue * multiplier;
  public static float ApplyPercentageRate(float baseValue, PercentageRange range, IRandomSource rng)
    => baseValue * range.SampleMultiplier(rng);
}
