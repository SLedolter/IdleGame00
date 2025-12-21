using System;

public sealed class SeededRandomSource : IRandomSource {
  private readonly Random rng;

  public SeededRandomSource(int seed) => rng = new Random(seed);
  public float NextFloat01() => (float)rng.NextDouble();
  public float Range(float min, float max) => min + (max - min) * NextFloat01();
  public int Range(int minInclusive, int maxInclusive) => rng.Next(minInclusive, maxInclusive);
}
