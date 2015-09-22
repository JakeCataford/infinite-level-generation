using UnityEngine;
using System.Collections;

public class CaveLayer : DensityProvider.Layer {
	private LibNoise.ModuleBase noise;

	public CaveLayer() {
		noise = new LibNoise.Generator.RidgedMultifractal (0.06, 2.0, 6, (int) (Random.value * 100000), LibNoise.QualityMode.Medium);
	}

	public float at(float x, float y, float z, float inDensity) {
		return (float) noise.GetValue (x,y,z);
	}
}
