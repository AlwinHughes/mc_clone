using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockyV2ChunkHost))]
public class BlockyV2HostEditor : IChunkHostEditor<BlockyChunkV2, BlockyGen1> {

  private Editor geom_noise_editor;
  private bool geom_noise_foldout;

  private Editor blocky_editor;
  private bool blocky_foldout;

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    BlockyV2ChunkHost ch = (BlockyV2ChunkHost) target;

    DrawSettingsEditor(ch.geom_noise_set, ch.onNoiseSetChange, ref geom_noise_foldout, ref geom_noise_editor);

    DrawSettingsEditor(ch.blocky_set, ch.onNoiseSetChange, ref blocky_foldout, ref blocky_editor);

  }

}
