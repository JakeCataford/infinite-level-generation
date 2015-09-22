using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	public Material material;
	public DensityProvider provider;

	void Start () {
		ConfigureProvider ();
		ChunkManager.init (provider, material);
	}

	void ConfigureProvider() {
		provider = new DensityProvider ();
		provider.AddLayer (new CaveLayer ());

	}
}
