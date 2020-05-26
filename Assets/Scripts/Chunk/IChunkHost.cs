using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IChunkHost<C> : MonoBehaviour where C : IChunk{

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

  abstract protected void init();

  abstract public void onNoiseSetChange();

  abstract public void onChunkSetChange();

  abstract public void onChunkHostSetChange();


  virtual protected GameObject getChunkObj(int i, int j) {
    return chunks[i + chunk_host_set.length * j];
  }

  virtual protected void setChunkObj(int i, int j, GameObject c) {
    chunks[i + chunk_host_set.length * j] = c;
  }

  abstract protected Vector3 getPos(int i, int j);
}
