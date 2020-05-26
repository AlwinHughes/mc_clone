using System.Collections;
using System;
using UnityEngine;

public class FlatChunkHost : IChunkHost<FlatChunk>  {


  [SerializeField]
  public GeomNoiseSettings geom_noise_set;


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

  private void updateAll() {
    for(int i = 0; i < chunk_host_set.length; i++) {
      for(int j = 0; j < chunk_host_set.width; j++) {
        GameObject obj = getChunkObj(i,j);
        obj.GetComponent<FlatChunk>().updateFromParent(
            getNoiseGen(i,j),
            chunk_set,
            Vector3.Scale(getPos(i,j), transform.localScale)
            );
        }
      }
  }


  private void resizeChunksArr() {
    if(chunks.Length > chunk_host_set.length * chunk_host_set.width) {
       for(int i = chunks.Length; i < chunk_host_set.length * chunk_host_set.width; i++) {
         DestroyImmediate(chunks[i]);
       }
    }


    GameObject[] temp = new GameObject[chunk_host_set.length * chunk_host_set.width];
    Array.Copy(chunks, 0, temp, 0, Math.Min(temp.Length, chunks.Length));
    chunks = temp;
  }

  private INoiseGenerator getNoiseGen(int i, int j) {
    return new NoiseGen1Geom(
        new NoiseSetting(noise_set, new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0)),
        geom_noise_set);
  }

  override protected Vector3 getPos(int i, int j) {
    return new Vector3(i,0,j);
  }

  void Start() {

  }

  // Update is called once per frame
  void Update()
  {

  }

  override public void onNoiseSetChange() {
    updateAll();
  }

  override public void onChunkSetChange() {
    updateAll();
  }

  override public void onChunkHostSetChange() {
    resizeChunksArr();
    init();
  }

}
