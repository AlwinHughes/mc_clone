using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatChunk : MonoBehaviour {

  public ChunkSettings chunk_set;
  public NoiseSetting noise_set;

  private int[] triangles;
  private Vector3[] verts;

  public INoiseGenerator noise_gen;

  [SerializeField]
  private GameObject mesh_obj;

  [SerializeField]
  private MeshFilter mesh_filter;

  void OnValidate() {

    Debug.Log("FlatChunk validate");

    if(chunk_set == null || noise_set == null) {
      Debug.Log("breaking");
      return;
    }

    if(mesh_obj != null && noise_gen != null && mesh_filter != null) {
    updateMesh();
    } else {
      init();
      updateMesh();
    }

  }


  void Initiate() {

  }

  private void init() {
    Debug.Log("init");

    mesh_obj = GameObject.Find("mesh");
    noise_gen = new NoiseGen1(noise_set);

    if(mesh_obj != null) {
      return;
    }
    
    mesh_obj = new GameObject("mesh");

    mesh_obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));

    mesh_filter = mesh_obj.AddComponent<MeshFilter>();
    mesh_filter.mesh = new Mesh();
    mesh_filter.transform.parent = transform;

  }

  private void updateMesh() {
    Debug.Log("updating");

    verts = new Vector3[chunk_set.res_x * chunk_set.res_y];
    triangles = new int[(chunk_set.res_x -1) * (chunk_set.res_y -1) * 6];

    int tri_index = 0;
    int vert_index;

    float inv_x_res = 1.0f / ( chunk_set.res_x - 1);
    float inv_y_res = 1.0f / ( chunk_set.res_y - 1);
  
    for(int i = 0; i < chunk_set.res_x; i++) {
      for(int j = 0; j < chunk_set.res_y; j++) {

        vert_index = i +  chunk_set.res_x * j;

        verts[vert_index] = new Vector3( 
            i * inv_x_res,
            noise_gen.sample2D(inv_x_res * i * noise_set.scale_x, inv_y_res * j * noise_set.scale_y),
            j * inv_y_res
         );

        if(i != chunk_set.res_x -1 && j != chunk_set.res_y -1) {
          
          triangles[tri_index] = vert_index;
          triangles[tri_index + 1] = vert_index + chunk_set.res_x;
          triangles[tri_index + 2] = vert_index + chunk_set.res_x + 1;

          triangles[tri_index + 3] = vert_index;
          triangles[tri_index + 4] = vert_index + chunk_set.res_x + 1;
          triangles[tri_index + 5] = vert_index +1;

          tri_index += 6;

        }
      }
    }

    mesh_filter.sharedMesh.Clear();
    mesh_filter.sharedMesh.vertices = verts;
    mesh_filter.sharedMesh.triangles = triangles;
    mesh_filter.sharedMesh.RecalculateNormals();
  }


  public void onNoiseSetChange() { 
    updateMesh();
  }

  public void onChunkSetChange() { 
    updateMesh();
  }


  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {

  }


}
