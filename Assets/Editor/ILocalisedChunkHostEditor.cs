using System.Collections;
using UnityEngine;
using UnityEditor;

/*
abstract public class ILocalisedChunkHostEditor<C,G> : Editor where C : IChunk where G : INoiseGenerator  {

  private Editor chunk_editor;
  private bool chunk_foldout;

  private Editor noise_editor;
  private bool noise_foldout;

  private Editor chunk_host_editor;
  private bool chunk_host_foldout;

  public virtual void OnInspectorGUI() {

    DrawDefaultInspector();

    ILocalisedChunkHost<C,G> ch = (ILocalisedChunkHost<C,G>) target;

    DrawSettingsEditor(ch.chunk_host_set, ch.onChunkHostSetChange, ref chunk_host_foldout, ref chunk_host_editor);

    DrawSettingsEditor(ch.chunk_set, ch.onChunkSetChange, ref chunk_foldout, ref chunk_editor);

    DrawSettingsEditor(ch.noise_set, ch.onNoiseSetChange, ref noise_foldout, ref noise_editor);

  }


  protected void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
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
*/
