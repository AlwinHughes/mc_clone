﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatChunk : MonoBehaviour {

  public ChunkSettings chunk_set;
  public NoiseSetting noise_set;

  private int[] triangles;
  private Vector3[] verts;

  public INoiseGenerator noise_gen;

  [SerializeField]
  private bool has_parent = false;

  //[SerializeField]
  //private GameObject mesh_obj;

  [SerializeField]
  private MeshFilter mesh_filter;

  [SerializeField]
  private MeshCollider mesh_collider;

  void OnValidate() {

    if(has_parent) {
      return;
    }

    Debug.Log("FlatChunk validate");

    if(chunk_set == null || noise_set == null) {
      Debug.Log("breaking");
      return;
    }

    if(noise_gen != null && mesh_filter != null) {
      updateMesh();
    } else {
      init();
      updateMesh();
    }
  }

  private void init() {
    Debug.Log("init");

    if(noise_set != null) {
      noise_gen = new NoiseGen1(noise_set);
    }

    MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
    if(mr == null) {
      gameObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
    }

    mesh_filter = gameObject.GetComponent<MeshFilter>();
    if(mesh_filter == null) {
      mesh_filter = gameObject.AddComponent<MeshFilter>();
    }

    mesh_collider = gameObject.GetComponent<MeshCollider>();
    if(mesh_collider == null) {
      mesh_collider = gameObject.AddComponent<MeshCollider>();
    }

    mesh_filter.mesh = new Mesh();
    //mesh_filter.transform.parent = transform;
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


  public void onNoiseSetChange() { 
    updateMesh();
  }

  public void onChunkSetChange() { 
    updateMesh();
  }

  public void createdByParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos) {
    Debug.Log("created by parent");
    //noise_set = ns;
    chunk_set = cs;
    has_parent = true;
    noise_gen = ng;

    init();
    updateMesh();
    transform.position = pos;
  }

  public void updateFromParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos) {
    //noise_set = ns;
    chunk_set = cs;
    transform.position = pos;
    noise_gen = ng;

    updateMesh();
   }


  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }


}
