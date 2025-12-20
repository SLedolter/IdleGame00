using UnityEngine;

[DisallowMultipleComponent]
public class SimpleBrain : MonoBehaviour {
  public Fighter CurrentTarget {  get; private set; }

  public Fighter self;

  private void Awake() {
    self = GetComponent<Fighter>();
    if(self == null) { Debug.LogError("SimpleBrain braucht Fighter-Komponente.", this); }
  }

  private void Update() {
    CurrentTarget = FindNearestEnemy();
  }

  private Fighter FindNearestEnemy() {
    if (self == null || self.Team == null) { return null; }

    Fighter best = null;
    float bestSqr = float.PositiveInfinity;

    foreach(var other in FighterRegistry.Fighters) {
      if(other == null || other == self) { continue; }
      if(other.Team == null || other.Health == null) { continue; }
      if(!other.Health.IsAlive) { continue; }
      if(other.Team.Id == self.Team.Id) { continue; }

      float sqr = (other.transform.position - transform.position).sqrMagnitude;
      if(sqr < bestSqr) {
        bestSqr = sqr;
        best = other;
      }
    }
    return best;
  }
}
