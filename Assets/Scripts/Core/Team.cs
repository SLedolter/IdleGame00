using UnityEngine;

public enum TeamId {
  TeamA = 0,
  TeamB = 1
}

public class Team : MonoBehaviour {
  [field: SerializeField] public TeamId Id { get; private set; } = TeamId.TeamA;

  void Start() {

  }

  void Update() {

  }
}
