using UnityEngine;

public interface IRandomSource {
  float NextFloat01(); // [0, 1]
  float Range(float min, float max);
  int Range(int minInclusive, int maxInclusive);
}
