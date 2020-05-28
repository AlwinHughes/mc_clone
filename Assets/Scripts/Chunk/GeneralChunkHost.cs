using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralChunkHost<C,G> : IChunkHost<C,G> where C : IChunkCollider where G : INoiseGenerator {

  override protected void init() {
    Debug.Log("host init");

    if(chunks == null || chunks.Length == 0) {
      Debug.Log("create chunks array");
      chunks = new GameObject[chunk_host_set.length * chunk_host_set.width];
    }

    if(chunks.Length != chunk_host_set.length * chunk_host_set.width) {
      Debug.Log("resizing chunk array");
      resizeChunksArr();
    }


    GameObject obj;
    for(int i = 0; i < chunk_host_set.length; i++) {
      for(int j = 0; j < chunk_host_set.width; j++) {
        obj = getChunkObj(i,j);
        if(obj == null) {
          obj = new GameObject();
          obj.transform.parent = transform;
          FlatChunk fc = obj.AddComponent<FlatChunk>();

          INoiseGenerator ng = getNoiseGen(i,j);

          fc.createdByParent(ng, chunk_set, getPos(i,j));
          setChunkObj(i,j,obj);
        } else {
          obj.GetComponent<FlatChunk>().createdByParent(
              getNoiseGen(i,j),
              chunk_set,
              Vector3.Scale(getPos(i,j),transform.localScale)
              );
        }
      }
    }
  }

  override public void onNoiseSetChange() {

  }

  override public void onChunkSetChange() {

  }

  override public void onChunkHostSetChange() {

  }

  override protected G getNoiseGen(int i, int j) {
    return null;
  }


  override protected Vector3 getPos(int i, int j) {
    return new Vector3();
  }

}
