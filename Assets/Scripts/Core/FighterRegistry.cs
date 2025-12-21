using System.Collections.Generic;
using UnityEngine;

public static class FighterRegistry {
  private static readonly List<HumanBase> fighters = new();

  public static IReadOnlyList<HumanBase> Fighters => fighters;

  public static void Register(HumanBase fighter) {
    if(fighter != null && !fighters.Contains(fighter)) {
      fighters.Add(fighter);
    }
  }

  public static void Unregister(HumanBase fighter) {
    fighters.Remove(fighter);
  }
}
