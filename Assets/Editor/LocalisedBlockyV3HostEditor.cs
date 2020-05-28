using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalisedBlockyV3Host))]
public class LocalisedBlockyV3HostEditor : Editor {

  private Editor chunk_editor;
  private bool chunk_foldout;

  private Editor noise_editor;
  private bool noise_foldout;

  private Editor chunk_host_editor;
  private bool chunk_host_foldout;

  private Editor geom_noise_editor;
  private bool geom_noise_foldout;

  private Editor blocky_editor;
  private bool blocky_foldout;

  private Editor loc_editor;
  private bool loc_foldout;

  public override void OnInspectorGUI() {

    DrawDefaultInspector();

    LocalisedBlockyV3Host ch = (LocalisedBlockyV3Host) target;

    DrawSettingsEditor(ch.chunk_host_set, ch.onChunkHostSetChange, ref chunk_host_foldout, ref chunk_host_editor);

    DrawSettingsEditor(ch.chunk_set, ch.onChunkSetChange, ref chunk_foldout, ref chunk_editor);

    DrawSettingsEditor(ch.noise_set, ch.onNoiseSetChange, ref noise_foldout, ref noise_editor);

    DrawSettingsEditor(ch.geom_noise_set, ch.onNoiseSetChange, ref geom_noise_foldout, ref geom_noise_editor);

    DrawSettingsEditor(ch.blocky_set, ch.onNoiseSetChange, ref blocky_foldout, ref blocky_editor);

    DrawSettingsEditor(ch.loc_set, ch.onLocSetChange, ref loc_foldout, ref loc_editor);

    

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


/*
[CustomEditor(typeof(LocalisedBlockyV3Host),true)]
public class LocalisedBlockyV3HostEditor : ILocalisedChunkHostEditor<BlockyChunkV3, BlockyGen1> {


  private Editor geom_noise_editor;
  private bool geom_noise_foldout;

  private Editor blocky_editor;
  private bool blocky_foldout;

  public override void OnInspectorGUI() {
    Debug.Log("lbv3h inspector");
    base.OnInspectorGUI();

     LocalisedBlockyV3Host ch = (LocalisedBlockyV3Host) target;


    DrawSettingsEditor(ch.geom_noise_set, ch.onNoiseSetChange, ref geom_noise_foldout, ref geom_noise_editor);

    DrawSettingsEditor(ch.blocky_set, ch.onNoiseSetChange, ref blocky_foldout, ref blocky_editor);


  }

}
*/
