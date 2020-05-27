using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IChunkCollider : IChunk {

  [SerializeField]
  protected MeshCollider mesh_collider;

  virtual protected void init() { 
    base.init();

    mesh_collider = gameObject.GetComponent<MeshCollider>();
    if(mesh_collider == null) {
      mesh_collider = gameObject.AddComponent<MeshCollider>();
    }

  }

}
