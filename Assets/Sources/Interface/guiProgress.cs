using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

class ColorBar {
	public Color Start;
	public Color End;



	public ColorBar(int start, int end) {
		Start = ToColor(start);
		End = ToColor(end);
	}

	public Color Lerp(float value) {
		return Color.Lerp(Start, End, value);
	}

	private Color ToColor(int HexVal) {
		byte R = (byte)((HexVal >> 16) & 0xFF);
		byte G = (byte)((HexVal >> 8) & 0xFF);
		byte B = (byte)((HexVal) & 0xFF);
		return new Color32(R, G, B, 255);
	}
}

public enum ColorsTypes {
	HEALTH,
	ENERGY,
	WATER,
	HUGER
}

[ExecuteInEditMode]
public class guiProgress : MonoBehaviour {

	public Image Bar;
	public ColorsTypes Type;

	Dictionary<ColorsTypes, ColorBar> Colors = new Dictionary<ColorsTypes, ColorBar>() {
		{ ColorsTypes.HEALTH, new ColorBar(0x6A1A1A, 0x185F58) },
		{ ColorsTypes.ENERGY, new ColorBar(0x6A1A1A, 0x8AA97A) },
		{ ColorsTypes.WATER,  new ColorBar(0x6A1A1A, 0x113F60) },
		{ ColorsTypes.HUGER,  new ColorBar(0x6A1A1A, 0xA2753E) }
	};

	public float Value {
		set {
			if (!_slider) return;
			_slider.value = value;
			Bar.color = Colors[Type].Lerp(value);
		}
	}

	private Slider _slider;

	
	// Use this for initialization
	void Start() {
		_slider = GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update() {
		if (Application.isEditor && Bar) {
			Bar.color = Colors[Type].Lerp(_slider.value);
		}
	}
}
