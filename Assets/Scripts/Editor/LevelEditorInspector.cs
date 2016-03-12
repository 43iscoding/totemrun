using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		LevelEditor script = (LevelEditor) target;
		if (GUILayout.Button("Construct"))
		{
			script.CreateLevelConfig();
		}
	}
}
