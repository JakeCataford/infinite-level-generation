using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DensityProvider {
	public interface Layer {
		float at(float X, float Y, float Z, float inputDensity);
	}

	public List<Layer> layers = new List<Layer> ();

	public float DensityAt(float x, float y, float z) {
		float density = -1.0f;
		foreach (Layer layer in layers) {
			density = layer.at(x,y,z,density);
		}

		return density;
	}

	public float DensityAt(Vector3 location) {
		return DensityAt (location.x, location.y, location.z);
	}

	public void AddLayer(Layer layer) {
		layers.Add (layer);
	}
}
