using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class HitFlash : MonoBehaviour {
  [SerializeField] private Health health;
  [SerializeField] private Transform visualsRoot;
  [SerializeField] private float flashDuration = 0.12f;
  [SerializeField] private Color flashColor = Color.red;

  private Renderer[] renderers;
  private MaterialPropertyBlock mpb;

  // Shader property name: URP Lit nutz meist _BaseColor, Built-in Standard nutzt _Color
  private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
  private static readonly int ColorId = Shader.PropertyToID("_Color");

  private Coroutine flashRoutine;

  private void Awake() {
    if (health == null) { health = GetComponentInParent<Health>(); }
    if (visualsRoot == null) { visualsRoot = transform; }

    renderers = visualsRoot.GetComponentsInChildren<Renderer>(true);
    mpb = new MaterialPropertyBlock();

    if (health != null) {
      health.Damaged += OnDamaged;
    } else {
      Debug.LogWarning("Hitflash: Keine Health-Komponente gefunden", this);
    }
  }

  private void OnDestroy() {
    if (health != null) {
      health.Damaged -= OnDamaged;
    }
  }

  private void OnDamaged(float amount) {
    if (flashRoutine != null) { StopCoroutine(flashRoutine); }
    flashRoutine = StartCoroutine(FlashCo());
  }

  private IEnumerator FlashCo() {
    // 1) sofort rot setzen
    SetColorAll(flashColor);

    // 2) kurz warten
    yield return new WaitForSeconds(flashDuration);

    // 3) zurücksetzen: PropertyBlock leeren (Renderer fällt auf Materialfarbe zurück)
    ClearAll();
    flashRoutine = null;
  }

  private void SetColorAll(Color color) {
    foreach (var r in renderers) {
      if (r == null) { continue; }

      r.GetPropertyBlock(mpb);

      // Versuche URP-Farbe, sonst Built-In
      if (r.sharedMaterial != null && r.sharedMaterial.HasProperty(BaseColorId)) {
        mpb.SetColor(BaseColorId, color);
      } else {
        mpb.SetColor(ColorId, color);
      }
      r.SetPropertyBlock(mpb);
    }
  }

  private void ClearAll() {
    foreach (var r in renderers) {
      if (r == null) { continue; }
      r.SetPropertyBlock(null);
    }
  }
}
