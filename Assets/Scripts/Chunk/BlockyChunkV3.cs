using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockyChunkV3 : IChunk {

  [SerializeField]
  private MeshCollider mesh_collider;

  [Range(1f,100f)]
  public float steps = 10f;

  private List<Vector3> verts2;
  private List<int> triangles2;

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

    //verts = new Vector3[4 * (chunk_set.res_x ) * (chunk_set.res_y )];
    //triangles = new int[(chunk_set.res_x -1) * (chunk_set.res_y -1) *6];

    verts2 = new List<Vector3>(4 * (chunk_set.res_x ) * (chunk_set.res_y ));
    triangles2 = new List<int>((chunk_set.res_x -1) * (chunk_set.res_y -1) *6);

    int tri_index = 0;
    int vert_index = 0;
    float inv_x_res = 1.0f / ( chunk_set.res_x - 1);
    float inv_y_res = 1.0f / ( chunk_set.res_y - 1);

    float[] noise = new float[(chunk_set.res_x ) * (chunk_set.res_y )];

    for(int i = 0; i < chunk_set.res_x; i++) {
      for(int j = 0; j < chunk_set.res_y; j++) {
        noise[i + chunk_set.res_x * j] = noise_gen.sample2D(inv_x_res * i, inv_y_res * j);
      }
    }

    float y;
    float y2;
    float y3;

    for(int i = 0; i < chunk_set.res_x; i++) {
      for(int j = 0; j < chunk_set.res_y; j++) {


        /*
           if(i == chunk_set.res_x -1 || j != chunk_set.res_y -1) {
           continue;
           }
           */


        if(i != chunk_set.res_x -1 && j != chunk_set.res_y -1) {

          y = noise[i + chunk_set.res_x * j];
         
          
          verts2.Add(new Vector3(i * inv_x_res, y, j * inv_y_res));
          verts2.Add(new Vector3( (i+1) * inv_x_res, y, j * inv_y_res));
          verts2.Add(new Vector3( i * inv_x_res, y, (j +1) * inv_y_res));
          verts2.Add(new Vector3( (i+1) * inv_x_res, y, (j+1) * inv_y_res));

          triangles2.Add(vert_index);
          triangles2.Add(vert_index + 3);
          triangles2.Add(vert_index + 1);

          triangles2.Add(vert_index);
          triangles2.Add(vert_index + 2);
          triangles2.Add(vert_index + 3);

          tri_index += 6;

          

          vert_index += 4;
          
          
          

          //vertical faces in x direction
          y2 = noise[(i + 1) + chunk_set.res_x * j];
          if(y2 != y) {

            verts2.Add(new Vector3( (i + 1) * inv_x_res, y2, j * inv_y_res));
            verts2.Add(new Vector3( (i + 1) * inv_x_res, y2, (j + 1) * inv_y_res));
            verts2.Add(new Vector3( (i + 1) * inv_x_res, y, j * inv_y_res));
            verts2.Add(new Vector3( (i + 1) * inv_x_res, y, (j + 1) * inv_y_res));

            triangles2.Add(vert_index);
            triangles2.Add(vert_index + 3);
            triangles2.Add(vert_index + 1);

            triangles2.Add(vert_index);
            triangles2.Add(vert_index + 2);
            triangles2.Add(vert_index + 3);

            tri_index += 6;

            vert_index += 4;
          }
          
          

          //vertical faces in the y direction
          y3 = noise[i + chunk_set.res_x * (j + 1)];
          if(y3 != y) {

            verts2.Add(new Vector3(i * inv_x_res, y3, (j + 1) * inv_y_res));
            verts2.Add(new Vector3((i + 1) * inv_x_res, y3, (j + 1) * inv_y_res));
            verts2.Add(new Vector3(i * inv_x_res, y, (j + 1) * inv_y_res));
            verts2.Add(new Vector3((i + 1) * inv_x_res, y, (j + 1) * inv_y_res));

            triangles2.Add(vert_index);
            triangles2.Add(vert_index + 1);
            triangles2.Add(vert_index + 3);

            triangles2.Add(vert_index);
            triangles2.Add(vert_index + 3);
            triangles2.Add(vert_index + 2);

            tri_index += 6;

            vert_index += 4;
          }

        }






      }
    }

    mesh_filter.sharedMesh.Clear();
    mesh_filter.sharedMesh.vertices = verts2.ToArray();
    mesh_filter.sharedMesh.triangles = triangles2.ToArray();
    mesh_filter.sharedMesh.RecalculateNormals();

    mesh_collider.sharedMesh = mesh_filter.sharedMesh;
  }


}
