using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class AmountClass
{
	public editorResouce Resource;
	public int Amount;

	public override string ToString()
	{
		return "id:" + Resource.Name + " amount:" + Amount.ToString();
	}
}

public class editorResouce : ScriptableObject
{
	public string Name;
	public string Description;

	public Sprite Icon;
	public gameLoot LootPrefab;

}

public class editorInstrument : editorResouce
{
	public int Hitpoint;
}

public class editorRecepie : editorResouce
{
	public AmountClass[] Ingredients;
	public int Amount;
	public float Time;
	public RecepieTypes Type = RecepieTypes.WORK;
}


[CustomEditor(typeof(editorResouce))]
public class ItemEditor : Editor
{

	public override void OnInspectorGUI()
	{
		editorResouce component = (editorResouce)target;

		//base.OnInspectorGUI();

		component.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", component.Icon, typeof(Sprite), true);

		EditorGUILayout.BeginVertical();
		component.Name = EditorGUILayout.TextField("Resource name", component.Name);
		component.Description = EditorGUILayout.TextField("Description", component.Description);
		component.LootPrefab = (gameLoot)EditorGUILayout.ObjectField("Loot prefab", component.LootPrefab, typeof(gameLoot), true);
		DrawCustomInspector();
		EditorGUILayout.EndVertical();
		DrawAfterCustom();
	}

	protected virtual void DrawCustomInspector()
	{
	}
	protected virtual void DrawAfterCustom()
	{
	}
}

[CustomEditor(typeof(editorInstrument))]
public class InstrumentEditor : ItemEditor
{
	protected override void DrawCustomInspector()
	{
		editorInstrument component = (editorInstrument)target;
		component.Hitpoint = EditorGUILayout.IntField("Hitpoint", component.Hitpoint);
	}
}

[CustomEditor(typeof(editorRecepie))]
public class RecepieEditor : ItemEditor
{
	private SerializedProperty Property;

	protected override void DrawCustomInspector()
	{
		editorRecepie component = (editorRecepie)target;
		component.Amount = EditorGUILayout.IntField("Result amount", component.Amount);
		component.Time = EditorGUILayout.FloatField("Working time", component.Time);
		component.Type = (RecepieTypes)EditorGUILayout.EnumMaskField("Recepie type", (RecepieTypes)component.Type);

		SerializedObject m_Object = new SerializedObject(target);
		Property = m_Object.FindProperty("Ingredients");
		EditorGUILayout.PropertyField(Property, true);
		m_Object.ApplyModifiedProperties();


	}
}

