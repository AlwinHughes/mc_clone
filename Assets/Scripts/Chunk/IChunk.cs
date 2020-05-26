using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IChunk : MonoBehaviour {

  public ChunkSettings chunk_set;
  public NoiseSetting noise_set;

  protected int[] triangles;
  protected Vector3[] verts;

  [SerializeField]
  protected bool has_parent = false;

  public INoiseGenerator noise_gen;

  [SerializeField]
  protected MeshFilter mesh_filter;


  abstract public void createdByParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos);

  abstract public void updateFromParent(INoiseGenerator ng, ChunkSettings cs, Vector3 pos);

  virtual protected void init() {
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

    mesh_filter.mesh = new Mesh();
  }

  abstract protected void updateMesh();

  virtual public void OnValidate() {

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

  abstract public void onNoiseSetChange();

  abstract public void onChunkSetChange();

}
