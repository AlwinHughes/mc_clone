using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class ILocalisedChunkHost <C,G> : IChunkHost<C,G> where C : IChunkCollider where G : INoiseGenerator {

  public LocalisedChunkSettings loc_set;

  protected List<int> loaded_index;

  [SerializeField]

  protected virtual List<int> findInRange() {
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

  override public void OnValidate() {

    Debug.Log("local validat");
    if(chunk_host_set == null || noise_set == null || chunk_set == null || loc_set == null) {
      Debug.Log("local break");
      return;
    }

    base.OnValidate();


  }

  override protected void init() {
    
    Debug.Log("Ilocalised init");

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

  virtual protected void updateLoaded() {
    //looks through the loaded index list and will update each of them
    int i;
    int j;
    foreach(int k in loaded_index) {
      i = k % chunk_host_set.length;
      j = k / chunk_host_set.length;
      getChunkObj(i,j).GetComponent<C>().updateFromParent(
          getNoiseGen(i,j),
          chunk_set,
          Vector3.Scale(getPos(i,j), transform.localScale)
          );
    }
  }

  protected void deloadOld() {
    //deletes gameobjects that are out of range
    //does not create new game objects
    //will also save the new list of loaded indexes
    List<int> new_index = findInRange();

    foreach(int i in loaded_index.Except(new_index)) {
      DestroyImmediate(chunks[i]);
    }

    loaded_index = new_index;
  }

  protected void ensureLoaded() {
    //goes through list of loaded indexes and creates new game objects if they are null. 
    //does not update existing game objects
    int i;
    int j;
    foreach(int k in loaded_index) {
      i = k % chunk_host_set.length;
      j = k / chunk_host_set.length;

      GameObject obj = getChunkObj(i,j);
      if(obj == null) {
        obj = new GameObject();
        obj.transform.parent = transform;
        C fc = obj.AddComponent<C>();
        fc.createdByParent(getNoiseGen(i,j),chunk_set, getPos(i,j));
        setChunkObj(i,j,obj);
      }

    }
  }


  virtual new public void onNoiseSetChange() {
    updateLoaded();
  }

  virtual new public void onChunkSetChange() {
    updateLoaded();
  }

  virtual new public void onChunkHostSetChange() {

    //deloadOld();

    
  }

  virtual public void onLocSetChange() {
    deloadOld();
    ensureLoaded();
  }

  virtual public void updateCenter(Vector3 cen) {
    loc_set.center = cen;
    onLocSetChange();
  }

  virtual protected float metric(Vector3 v) {
    return Vector3.Distance(loc_set.center, v);
  }
}
