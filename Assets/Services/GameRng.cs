public static class GameRng {
  public static IRandomSource Current { get; private set; } = new UnityRandomSource();

  public static void Configure(RandomMode rndMode, int seed) {
    Current = rndMode switch {
      RandomMode.Off => new NoRandomSource(),
      RandomMode.Seeded => new SeededRandomSource(seed),
      _ => new UnityRandomSource()
    };
  }
}