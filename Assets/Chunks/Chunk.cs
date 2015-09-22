using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {
	public DensityProvider densityProvider;
	public Material mat;

	Transform player;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		JobQueue.QueueJob(new MeshGenerationJob(this, densityProvider, transform.position, OnChunkGeometryReady));
	}

	void OnChunkGeometryReady(MeshProxy proxy) {
		Mesh mesh = proxy.toMesh ();
		MeshCollider collider = gameObject.AddComponent<MeshCollider> ();
		collider.sharedMesh = mesh;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();
		mesh.RecalculateNormals ();
		meshFilter.mesh = mesh;

		
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();
		meshRenderer.material = mat;
	}

	public static Chunk createWithProvider(DensityProvider densityProvider, Material mat, Vector3 position) {
		GameObject chunk = (GameObject) new GameObject ();
		chunk.name = "chunk";
		chunk.transform.position = position;
		Chunk chunkComponent = chunk.AddComponent<Chunk> ();
		chunkComponent.densityProvider = densityProvider;

		chunkComponent.mat = mat;
		return chunkComponent;
	}
}
