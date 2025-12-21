public sealed class NoRandomSource : IRandomSource {
  public float NextFloat01() => 0.5f; // Mitte
  public float Range(float min, float max) => (min + max) * 0.5f;
  public int Range(int minInclusive, int maxExclusive) => minInclusive; // deterministisch
}
