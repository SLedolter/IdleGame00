using UnityEngine;

[DisallowMultipleComponent]
public sealed class MoveToTarget : MonoBehaviour {
  [SerializeField] private float moveSpeed = 2.5f;
  [SerializeField] private float stoppingDistance = 1.1f;

  private SimpleBrain brain;

  private void Awake() {
    brain = GetComponent<SimpleBrain>();
    if(brain == null ) { Debug.LogError("MoveToTarget braucht Simple-Brain-Komponente");  }
  }

  private void Update() {
    var target = brain.CurrentTarget;
    if( target == null ) { return; }

    Vector3 toTarget = target.transform.position - transform.position;
    toTarget.y = 0f;

    float dist = toTarget.magnitude;
    if( dist < stoppingDistance ) { return;  }

    Vector3 dir = toTarget / dist;
    transform.position += dir * (moveSpeed * Time.deltaTime); 

    // optional: ansehen
    if(dir.sqrMagnitude > 0.0001f) { transform.forward = dir; }
  }
}
