using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatChunk : IChunk {

  [SerializeField]
  private MeshCollider mesh_collider;

  override protected void init() {

    base.init();

    mesh_collider = gameObject.GetComponent<MeshCollider>();
    if(mesh_collider == null) {
      mesh_collider = gameObject.AddComponent<MeshCollider>();
    }

    //mesh_filter.transform.parent = transform;
  }

  override protected void updateMesh() {
    Debug.Log("updating");

    verts = new Vector3[chunk_set.res_x * chunk_set.res_y];
    triangles = new int[(chunk_set.res_x -1) * (chunk_set.res_y -1) * 6];

    int tri_index = 0;
    int vert_index;
float inv_x_res = 1.0f / ( chunk_set.res_x - 1);
    float inv_y_res = 1.0f / ( chunk_set.res_y - 1);


    for(int i = 0; i < chunk_set.res_x; i++) {
      for(int j = 0; j < chunk_set.res_y; j++) {

        vert_index = i + chunk_set.res_x * j;

        verts[vert_index] = new Vector3( 
            i * inv_x_res,
            noise_gen.sample2D(inv_x_res * i, inv_y_res * j),
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

    mesh_collider.sharedMesh = mesh_filter.sharedMesh;
  }


  override public void onNoiseSetChange() { 
    updateMesh();
  }

  override public void onChunkSetChange() { 
    updateMesh();
  }

  override public void createdByParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos) {
    Debug.Log("created by parent");
    //noise_set = ns;
    chunk_set = cs;
    has_parent = true;
    noise_gen = ng;

    init();
    updateMesh();
    transform.position = pos;
  }

  override public void updateFromParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos) {
    //noise_set = ns;
    chunk_set = cs;
    transform.position = pos;
    noise_gen = ng;

    updateMesh();
   }

}
