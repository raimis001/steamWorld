#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public static class ItemsDatabase
{
 
	[MenuItem("Game/Create/Loot")]
	public static void CreateLoot()
	{
		Create<editorResouce>();
	}
	[MenuItem("Game/Create/Instrument")]
	public static void CreateInstrument()
	{
		Create<editorInstrument>();
	}
	[MenuItem("Game/Create/Recepie")]
	public static void CreateRecepie()
	{
		Create<editorRecepie>();
	}

	public static void Create<T>() where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
#endif