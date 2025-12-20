using System.Collections.Generic;
using UnityEngine;

public static class FighterRegistry {
  private static readonly List<Fighter> fighters = new();

  public static IReadOnlyList<Fighter> Fighters => fighters;

  public static void Register(Fighter fighter) {
    if(fighter != null && !fighters.Contains(fighter)) {
      fighters.Add(fighter);
    }
  }

  public static void Unregister(Fighter fighter) {
    fighters.Remove(fighter);
  }
}
