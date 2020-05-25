using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlatChunk))]
public class ChunkEditor : Editor {

  private Editor chunk_editor;
  private bool chunk_foldout;

  private Editor noise_editor;
  private bool noise_foldout;

   public override void OnInspectorGUI() { 
    DrawDefaultInspector();

    FlatChunk c = (FlatChunk) target;

    DrawSettingsEditor(c.chunk_set, c.onChunkSetChange, ref chunk_foldout, ref chunk_editor);
    DrawSettingsEditor(c.noise_set, c.onNoiseSetChange, ref noise_foldout, ref noise_editor);

   }



  void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
  {
    if (settings != null)
    {
      foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
      using (var check = new EditorGUI.ChangeCheckScope())
      {
        if (foldout)
        {
          CreateCachedEditor(settings, null, ref editor);
          editor.OnInspectorGUI();

          if (check.changed)
          {
            if (onSettingsUpdated != null)
            {
              onSettingsUpdated();
            }
          }
        }
      }
    }
  }



}
