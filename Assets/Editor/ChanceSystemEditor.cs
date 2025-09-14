#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChanceTable))]
public class ChanceTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var table = (ChanceTable)target;

        var total = table.GetTotalChance();

        if (Mathf.Approximately(total, 100f))
            EditorGUILayout.HelpBox("✅ Total chance = 100%", MessageType.Info);
        else
            EditorGUILayout.HelpBox($"⚠️ Total = {total}, must be 100!", MessageType.Warning);
    }
}
#endif