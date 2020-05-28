using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

abstract public class IChunkHost<C,G> : MonoBehaviour where C : IChunk where G : INoiseGenerator {

  [SerializeField]
  protected GameObject[] chunks;

  [SerializeField]
  public ChunkHostSettings chunk_host_set;
  [SerializeField]
  public ChunkSettings chunk_set;
  [SerializeField]
  public NoiseSetting noise_set;

  virtual public void OnValidate() {
    Debug.Log("host validat");
    if(chunk_host_set == null || noise_set == null || chunk_set == null) {
      Debug.Log("host break");
      return;
    }

    init();
  }

  virtual protected void init() { 

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
          C fc = obj.AddComponent<C>();

          INoiseGenerator ng = getNoiseGen(i,j);

          fc.createdByParent(ng, chunk_set, getPos(i,j));
          setChunkObj(i,j,obj);
        } else {
          obj.GetComponent<C>().createdByParent(
              getNoiseGen(i,j),
              chunk_set,
              Vector3.Scale(getPos(i,j),transform.localScale)
              );
        }
      }
    }
  }

  virtual public void onNoiseSetChange() {
    updateAll();
  }

  virtual public void onChunkSetChange() {
    updateAll();
  }

  virtual public void onChunkHostSetChange() {
    resizeChunksArr();
    updateAll();
  }


  virtual protected GameObject getChunkObj(int i, int j) {
    return chunks[i + chunk_host_set.length * j];
  }

  virtual protected void setChunkObj(int i, int j, GameObject c) {
    chunks[i + chunk_host_set.length * j] = c;
  }

  virtual protected Vector3 getPos(int i, int j) {

    return new Vector3(i,0,j);
  }

  virtual protected void updateAll() {
    for(int i = 0; i < chunk_host_set.length; i++) {
      for(int j = 0; j < chunk_host_set.width; j++) {
        GameObject obj = getChunkObj(i,j);
        obj.GetComponent<C>().updateFromParent(
            getNoiseGen(i,j),
            chunk_set,
            Vector3.Scale(getPos(i,j), transform.localScale)
            );
        }
      }
  }

  abstract protected G getNoiseGen(int i, int j);

  virtual protected void resizeChunksArr() {
    if(chunks.Length > chunk_host_set.length * chunk_host_set.width) {
       for(int i = chunks.Length; i < chunk_host_set.length * chunk_host_set.width; i++) {
         DestroyImmediate(chunks[i]);
       }
    }

    GameObject[] temp = new GameObject[chunk_host_set.length * chunk_host_set.width];
    Array.Copy(chunks, 0, temp, 0, Math.Min(temp.Length, chunks.Length));
    chunks = temp;
  }


}
