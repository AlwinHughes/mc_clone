using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocky3DChunk : IChunkCollider {

  private List<Vector3> verts2;
  private List<int> triangles2;

  private float threshold = 0.0f;

  private List<Vector2> uvs;

  private Vector2 posx = new Vector2(0f,0f);
  private Vector2 negx = new Vector2(1f/3,0f);
  private Vector2 posy = new Vector2(2f/3,0f);
  private Vector2 negy = new Vector2(0f,0.5f);
  private Vector2 posz = new Vector2(1f/3,0.5f);
  private Vector2 negz = new Vector2(2f/3,0.5f);

  private Vector2 bot_right = new Vector2(0.25f, 0f);
  private Vector2 top_left = new Vector2(0.0f,0.5f);
  private Vector2 top_right = new Vector2(0.25f, .5f);

  override protected void updateMesh() {

    verts2 = new List<Vector3>(4 * (chunk_set.res_x ) * (chunk_set.res_y ));
    triangles2 = new List<int>((chunk_set.res_x -1) * (chunk_set.res_y -1) *6);

    uvs = new List<Vector2>(4 * (chunk_set.res_x ) * (chunk_set.res_y ));

    float[] noise = new float[(chunk_set.res_x ) * (chunk_set.res_y ) * (chunk_set.res_z)];

    int tri_index = 0;
    int vert_index = 0;
    float inv_x_res = 1.0f / ( chunk_set.res_x - 1);
    float inv_y_res = 1.0f / ( chunk_set.res_y - 1);
    float inv_z_res = 1.0f / ( chunk_set.res_z - 1);

    for(int i = 0; i < chunk_set.res_x; i++) {
      for(int j = 0; j < chunk_set.res_y; j++) {
        for(int k = 0; k < chunk_set.res_z; k++) {
          noise[i + chunk_set.res_x * ( j + chunk_set.res_y * k)] = noise_gen.sample3D(inv_x_res * i, inv_y_res * j, inv_z_res * k);
        }
      }
    }

    float above_threshold;

    for(int k = 0; k < chunk_set.res_z; k++) {
      threshold =((float) k) / chunk_set.res_z - 0.5f;
      above_threshold = ((float) k +1 ) / chunk_set.res_z - 0.5f;


      for(int i = 0; i < chunk_set.res_x; i++) {
        for(int j = 0; j < chunk_set.res_y; j++) {



          if(i != chunk_set.res_x -1 && j != chunk_set.res_y -1 && k != chunk_set.res_z -1) {


            float w = noise[i + chunk_set.res_x * ( j + chunk_set.res_y * k)];


            //the one above it
            float w_above = noise[i + chunk_set.res_x * ( j + chunk_set.res_y * (k+1))];

            float w_x  = noise[i + 1 + chunk_set.res_x * ( j + chunk_set.res_y * k)];

            float w_y = noise[i + chunk_set.res_x * ( (j + 1) + chunk_set.res_y * k)];


            if(w > threshold) {
              //current position is air


              if(w_above < above_threshold) {
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j +1)));
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(negy);
                uvs.Add(negy + top_left);
                uvs.Add(negy + top_right);
                uvs.Add(negy + bot_right);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 3);
                triangles2.Add(vert_index + 2);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 2);
                triangles2.Add(vert_index + 1);

                tri_index += 6;
                vert_index += 4;
              }


              if(w_x < threshold) {
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(negx);
                uvs.Add(negx + top_left);
                uvs.Add(negx + bot_right);
                uvs.Add(negx + top_right);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 1);
                triangles2.Add(vert_index + 3);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 3);
                triangles2.Add(vert_index + 2);

                tri_index += 6;
                vert_index += 4;

              }


              if(w_y < threshold) {

                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(negz);
                uvs.Add(negz + top_left);
                uvs.Add(negz + bot_right);
                uvs.Add(negz + top_right);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 3);
                triangles2.Add(vert_index + 1);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 2);
                triangles2.Add(vert_index + 3);

                tri_index += 6;
                vert_index += 4;

              }

            }



            if(w <= threshold) {
              //current position is air

              if(w_above >= threshold) {
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * (j +1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(posy);
                uvs.Add(posy + top_left);
                uvs.Add(posy + bot_right);
                uvs.Add(posy + top_right);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 1);
                triangles2.Add(vert_index + 3);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 3);
                triangles2.Add(vert_index + 2);

                tri_index += 6;
                vert_index += 4;
              }


              if(w_x >= threshold) {
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * j));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(posx);
                uvs.Add(posx + top_left);
                uvs.Add(posx + bot_right);
                uvs.Add(posx + top_right);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 3);
                triangles2.Add(vert_index + 1);

                triangles2.Add(vert_index);
                triangles2.Add(vert_index + 2);
                triangles2.Add(vert_index + 3);

                tri_index += 6;
                vert_index += 4;

              }

              if(w_y >= threshold) {

                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * i, inv_z_res * (k + 1), inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * k, inv_y_res * (j + 1)));
                verts2.Add(new Vector3(inv_x_res * (i + 1), inv_z_res * (k + 1), inv_y_res * (j + 1)));

                uvs.Add(posz);
                uvs.Add(posz + top_left);
                uvs.Add(posz + bot_right);
                uvs.Add(posz + top_right);

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
      }
    }

    mesh_filter.sharedMesh.Clear();
    mesh_filter.sharedMesh.vertices = verts2.ToArray();
    mesh_filter.sharedMesh.triangles = triangles2.ToArray();
    mesh_filter.sharedMesh.uv= uvs.ToArray();
    mesh_filter.sharedMesh.RecalculateNormals();

    mesh_collider.sharedMesh = mesh_filter.sharedMesh;
  }

  private int getYPos(int i, int j, int k)  {
    return i + chunk_set.res_x * ( j + chunk_set.res_y * (k+1));
  }

  private int getXPox(int i, int j, int k)  {
    return i + 1 + chunk_set.res_x * ( j + chunk_set.res_y * k);
  }

  private int getZPos(int i, int j, int k)  {
    return i + 1 + chunk_set.res_x * ( j + chunk_set.res_y * k);
  }


}
