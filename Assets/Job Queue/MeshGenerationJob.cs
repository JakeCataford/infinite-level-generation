using UnityEngine;
using System;

public class MeshGenerationJob : Job {
	Action<MeshProxy> callback;
	DensityProvider provider;
	Vector3 offset;
	MeshProxy proxy;
	Chunk chunk;
	public MeshGenerationJob (Chunk chunk, DensityProvider provider, Vector3 offset, Action<MeshProxy> callback) {
		this.provider = provider;
		this.offset = offset;
		this.callback = callback;
		this.chunk = chunk;
	}

	public void work() {
		MarchingCubes.SetTarget (0.0f);
		MarchingCubes.SetWindingOrder (0, 1, 2);
		MarchingCubes.SetModeToCubes ();
		proxy = MarchingCubes.MakeMeshProxy(voxels());
	}

	public void finish() {
		if (chunk) {
			callback.Invoke (proxy);
		}
	}

	private float[,,] voxels() {
		var width  = 17;
		var height = 17;
		var length = 17;
		
		var voxels = new float[width, height, length];
		
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				for(int z = 0; z < length; z++)
				{
					voxels[x,y,z] = (float) provider.DensityAt(new Vector3(x,y,z) + offset);
				}
			}
		}
		
		return voxels;
	}
}
