using UnityEngine;
using static IDamageable;

[DisallowMultipleComponent]
public sealed class Fighter : MonoBehaviour {
  public Team Team;
  public Health Health;

  private void Awake() {
    Team = GetComponent<Team>();
    Health = GetComponent<Health>();

    if(Team == null ) { Debug.LogError("Fighter braucht Team-Komponente.", this);  }
    if(Health == null ) { Debug.LogError("Fighter braucht Health-Komponente.", this); }
  }

  private void OnEnable() => FighterRegistry.Register(this);
  private void OnDisable() => FighterRegistry.Unregister(this);
}
