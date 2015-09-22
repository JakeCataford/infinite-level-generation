using UnityEngine;
using System.Collections;

public class MeshProxy {
	public int[] triangles;
	public Vector3[] vertices;
	public Vector3[] normals;



	public Mesh toMesh() {
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		return mesh;
	}
}
