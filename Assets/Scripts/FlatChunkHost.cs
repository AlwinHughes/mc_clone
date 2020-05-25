﻿using System.Collections;
using System;
using UnityEngine;

public class FlatChunkHost : MonoBehaviour {


  [SerializeField]
  private GameObject[] chunks;

  [SerializeField]
  public ChunkHostSettings chunk_host_set;
  [SerializeField]
  public ChunkSettings chunk_set;
  [SerializeField]
  public NoiseSetting noise_set;



  void OnValidate() {
    Debug.Log("host validat");
    if(chunk_host_set == null || noise_set == null || chunk_set == null) {
      Debug.Log("host break");
      return;
    }

    init();
  }


  private void init() {
    if(chunks == null) {
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
          Debug.Log("item " + i + ", " + j + "is null, recreating");
          obj = new GameObject();
          obj.transform.parent = transform;
          FlatChunk fc = obj.AddComponent<FlatChunk>();
          fc.createdByParent(getNoiseSetting(i,j), chunk_set, getPos(i,j));
          setChunkObj(i,j,obj);
        } else {
          obj.GetComponent<FlatChunk>().createdByParent(getNoiseSetting(i,j), chunk_set, getPos(i,j));
        }
      }
    }
  }

  private void updateAll() {
    for(int i = 0; i < chunk_host_set.length; i++) {
      for(int j = 0; j < chunk_host_set.width; j++) {
        GameObject obj = getChunkObj(i,j);
        obj.GetComponent<FlatChunk>().updateFromParent(getNoiseSetting(i,j), chunk_set, getPos(i,j));
        }
      }
  }


  private void resizeChunksArr() {
    GameObject[] temp = new GameObject[chunk_host_set.length * chunk_host_set.width];
    Array.Copy(chunks, 0, temp, 0, Math.Min(temp.Length, chunks.Length));
    chunks = temp;
  }

  private NoiseSetting getNoiseSetting(int i, int j) {
    return new NoiseSetting(noise_set, new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0));
  }

  private Vector3 getPos(int i, int j) {
    return new Vector3(i,0,j);
  }


  void Start() {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void onNoiseSetChange() {
    updateAll();
  }

  public void onChunkSetChange() {
    updateAll();
  }

  public void onChunkHostSetChange() {
    resizeChunksArr();
    init();
  }

  private GameObject getChunkObj(int i, int j) {
    return chunks[i + chunk_host_set.length * j];
  }

  private void setChunkObj(int i, int j, GameObject c) {
    chunks[i + chunk_host_set.length * j] = c;
  }
}