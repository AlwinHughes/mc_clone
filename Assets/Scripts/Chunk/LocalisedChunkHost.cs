using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class LocalisedChunkHost <C,G> : IChunkHost<C,G> where C : IChunk where G : INoiseGenerator {

  public LocalisedChunkSettings loc_set;

  protected List<int> loaded_index;

  protected Vector3 center = new Vector3();

  public virtual List<int> findInRange() {
    List<int> temp = new List<int>();

    for(int i = 0; i < chunk_host_set.length; i++) {
      for(int j = 0; j < chunk_host_set.width; j++) {
        if(metric(Vector3.Scale(getPos(i,j), transform.localScale)) < loc_set.range) {
          temp.Add(i + chunk_host_set.length * j);
        }
      }
    }
    return temp;
  }

  virtual new public void OnValidate() {
    Debug.Log("host validat");
    if(chunk_host_set == null || noise_set == null || chunk_set == null || loc_set == null) {
      Debug.Log("host break");
      return;
    }

  }

  virtual new protected void init() {

    if(chunks == null || chunks.Length == 0) {
      Debug.Log("create chunks array");
      chunks = new GameObject[chunk_host_set.length * chunk_host_set.width];
    }

    if(chunks.Length != chunk_host_set.length * chunk_host_set.width) {
      Debug.Log("resizing chunk array");
      resizeChunksArr();
    }

    loaded_index = findInRange();

    int i;
    int j;
    GameObject obj;
  
    foreach(int k in loaded_index) {
      i = k % chunk_host_set.length;
      j = k / chunk_host_set.length; 

      obj = getChunkObj(i,j);
      if(obj == null) {
        obj = new GameObject();
        obj.transform.parent = transform;
        C fc = obj.AddComponent<C>();

        fc.createdByParent(getNoiseGen(i,j),chunk_set, getPos(i,j));
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

  virtual protected void updateInRange() {

    int i;
    int j;
    GameObject obj;
    foreach(int k in loaded_index) {
      i = k % chunk_host_set.length;
      j = k / chunk_host_set.length; 

      obj = getChunkObj(i,j);
      if(obj == null) {
        obj = new GameObject();
        obj.transform.parent = transform;
        C fc = obj.AddComponent<C>();

        fc.createdByParent(getNoiseGen(i,j),chunk_set, getPos(i,j));
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

  protected void deloadOld() {
    List<int> new_index = findInRange();

    foreach(int i in loaded_index.Except(new_index)) {
      Destroy(chunks[i]);
    }

    loaded_index = new_index;
  }


  virtual new public void onNoiseSetChange() {
    updateInRange();
  }

  virtual new public void onChunkSetChange() {
    updateInRange();
  }

  virtual new public void onChunkHostSetChange() {
    loaded_index = findInRange();
    updateInRange();
  }

  virtual public void updateCenter(Vector3 cen) {
    center = cen;
    loaded_index = findInRange();
    updateInRange();
  }

  virtual protected float metric(Vector3 v) {
    return Vector3.Distance(center, v);
  }




}
