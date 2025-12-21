using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer))]
public sealed class RangeIndicator : MonoBehaviour {
  [Header("Source")]
  [SerializeField] private MeleeAttack meleeAttack;

  [Header("Visual")]
  [SerializeField] private int segments = 48;
  [SerializeField] private float yOffset = 0.03f; // gegen z-fighting
  [SerializeField] private float lineWidth = 0.03f;
  [SerializeField] private bool showAlways = true;

  private LineRenderer lineRenderer;

  private void Awake() {
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.useWorldSpace = false;
    lineRenderer.loop = true;
    lineRenderer.widthMultiplier = lineWidth;

    // Falls du keine Material-Assets anlegen willst: Unity legt manchmal eins an,
    // sonst musst du im Inspector eins zuweisen (z.B. "Sprites/Default").
    if (lineRenderer.sharedMaterial == null) {
      // notfalls mal im inspector testen
    }
  }
  private void OnEnable() => Rebuild();

  private void Update() {
    // minimal: an/aus schalten. später zb nur wenn selektiert
    lineRenderer.enabled = showAlways;
  }

  public void Rebuild() {
    float radius = meleeAttack != null ? meleeAttack.Range : 1.2f;

    lineRenderer.positionCount = Mathf.Max(8, segments);
    for(int i = 0; i < lineRenderer.positionCount; i++) {
      float t = (float)i / lineRenderer.positionCount;
      float ang = t * Mathf.PI * 2f;

      float x = Mathf.Cos(ang) * radius;
      float z = Mathf.Sin(ang) * radius;

      lineRenderer.SetPosition(i, new Vector3(x, yOffset, z));
    }
  }
#if UNITY_EDITOR
  private void OnValidate() {
    if (!Application.isPlaying  && lineRenderer == null) {
      lineRenderer = GetComponent<LineRenderer>();
    }
    if(lineRenderer != null) {
      lineRenderer.widthMultiplier = lineWidth;
      Rebuild();
    }
  }
#endif
}

