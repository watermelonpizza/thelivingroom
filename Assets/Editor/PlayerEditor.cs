using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private Player _player;
    private Editor _movementSettingsEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if (check.changed)
            {
                //_player.GeneratePlanet();
            }
        }

        DrawSettingsEditor(_player.movementSettings, () => { }, ref _player.movementSettingsFoldout, ref _movementSettingsEditor);
    }

    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings == null)
        {
            return;
        }

        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (foldout)
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();

                if (check.changed && onSettingsUpdated != null)
                {
                    onSettingsUpdated.Invoke();
                }
            }
        }
    }

    private void OnEnable()
    {
        _player = (Player)target;
    }
}
