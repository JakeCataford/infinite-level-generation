using UnityEngine;
using System;
using System.Collections.Generic;

public class ChunkManager : Singleton<ChunkManager> {
	Dictionary<Vector3, Chunk> chunks = new Dictionary<Vector3, Chunk>();
	private Transform _player;

	private Transform Player {
		get {
			if(_player == null) {
				_player = GameObject.FindGameObjectWithTag("Player").transform;
			}

			return _player;
		}
	}

	public DensityProvider provider;
	public Material material;
	int chunkSize = 16;
	int loadRange = 3;

	void Update() {
		for(int i = -loadRange; i < loadRange; i++) {
			for(int j = -loadRange; j < loadRange; j++) {
				for(int k = -loadRange; k < loadRange; k++) {
					Vector3 key = new Vector3(i,j,k) + playerChunk();
					if(!chunks.ContainsKey(key)) {
						chunks.Add(key, Chunk.createWithProvider(provider, material, new Vector3(key.x * chunkSize, key.y * chunkSize, key.z * chunkSize)));
					}
				}
			}
		}

		List<Vector3> unloadableChunks = new List<Vector3>();
		foreach(KeyValuePair<Vector3, Chunk> kvp in chunks) {
			Vector3 playerChunke = playerChunk();
			if(Mathf.Abs(kvp.Key.x - playerChunke.x) > loadRange ||
			   Mathf.Abs(kvp.Key.y - playerChunke.y) > loadRange ||
			   Mathf.Abs(kvp.Key.z - playerChunke.z) > loadRange) {
				unloadableChunks.Add(kvp.Key);
			}
		}

		foreach (Vector3 key in unloadableChunks) {
			Destroy(chunks[key].gameObject);
			chunks.Remove(key);
		}
	}

	public Vector3 playerChunk() {
		Vector3 result = new Vector3 ();
		result.x = Mathf.RoundToInt (Player.position.x / chunkSize);
		result.y = Mathf.RoundToInt (Player.position.y / chunkSize);
		result.z = Mathf.RoundToInt (Player.position.z / chunkSize);

		return result;
	}

	public static void init(DensityProvider provider, Material material) {
		Instance.provider = provider;
		Instance.material = material;
	}

	int roundToNearest(int roundTo, int value){
		return Mathf.RoundToInt(value/roundTo)*roundTo;
	}
}
