using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlatChunkHost))]
public class FlatChunkHostEditor : Editor {

  private Editor chunk_editor;
  private bool chunk_foldout;

  private Editor noise_editor;
  private bool noise_foldout;

  private Editor chunk_host_editor;
  private bool chunk_host_foldout;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    FlatChunkHost fch = (FlatChunkHost) target;

    DrawSettingsEditor(fch.chunk_host_set, fch.onChunkHostSetChange, ref chunk_host_foldout, ref chunk_host_editor);

    DrawSettingsEditor(fch.chunk_set, fch.onChunkSetChange, ref chunk_foldout, ref chunk_editor);
    DrawSettingsEditor(fch.noise_set, fch.onNoiseSetChange, ref noise_foldout, ref noise_editor);
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
