using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockyV1ChunkHost))]
public class BlockyV1HostEditor : IChunkHostEditor< FlatChunk, BlockyGen1>{


  private Editor geom_noise_editor;
  private bool geom_noise_foldout;

  private Editor blocky_editor;
  private bool blocky_foldout;

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    BlockyV1ChunkHost ch = (BlockyV1ChunkHost) target;

    DrawSettingsEditor(ch.geom_noise_set, ch.onNoiseSetChange, ref geom_noise_foldout, ref geom_noise_editor);

    DrawSettingsEditor(ch.blocky_set, ch.onNoiseSetChange, ref blocky_foldout, ref blocky_editor);

  }

}
