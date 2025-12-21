using UnityEngine;

public sealed class UnityRandomSource : IRandomSource {
  public float NextFloat01() => Random.value;
  public float Range(float min, float max) => Random.Range(min, max);
  public int Range(int minInclusive, int maxInclusifve) => Random.Range(minInclusive, maxInclusifve);
}
