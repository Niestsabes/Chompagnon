using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platformer.Mechanics.TombObject))]
[CanEditMultipleObjects]
public class TombTpSpawnPoint : Editor
{
    public void OnSceneGUI()
    {

        Platformer.Mechanics.TombObject tomb = (Platformer.Mechanics.TombObject)target;
        EditorGUI.BeginChangeCheck();

        Vector2 newPosition = Handles.PositionHandle(tomb.TombTpSpawnPoint, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Update spawn position");
            tomb.TombTpSpawnPoint = newPosition;
        }
    }
}
