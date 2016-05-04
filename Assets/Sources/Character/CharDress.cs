using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CharDress : MonoBehaviour
{

	public Material CoutMaterial;
	public Material ShirtMaterial;
	public Material PantMaterial;
	public Material GloveMaterial;
	public Material SkinMaterial;
	public Material HatMaterial;

	[Header("Body")]
	public GameObject Cout;
	public GameObject Shoulders;
	public GameObject HandLeft;
	public GameObject HaneRight;
	public GameObject Shirt;

	[Header("Pants")]
	public GameObject Hip;
	public GameObject LegLeft;
	public GameObject LegRight;

	[Header("Gloves")]
	public GameObject GloveLeft;
	public GameObject GloveRight;

	[Header("Hat")]
	public GameObject Hat;
	public GameObject HatCilinder;

	[Header("Show dress")]
	public bool ShowCout = true;
	public bool ShowGloves = false;
	public bool ShowHat = true;

	bool _showCout = true;
	bool showCout {
		get { return _showCout; }
		set {
			if (_showCout == value) return;
			_showCout = value;
			if (Cout) Cout.SetActive(_showCout);
			_bodyMaterial = _showCout ? CoutMaterial : ShirtMaterial;
			SetBodyMaterial();
		}
	}

	bool _showGloves = false;
	bool showGloves {
		get { return _showGloves; }
		set {
			if (_showGloves == value) return;
			_showGloves = value;
			_gloveMaterial = _showGloves ? GloveMaterial : SkinMaterial;
			SetGloweMaterial();
		}
	}

	bool _showHat = true;
	bool showHat {
		get { return _showCout; }
		set {
			if (_showHat == value) return;
			_showHat = value;
			if (Hat) Hat.SetActive(_showHat);

		}
	}

	Material _bodyMaterial = null;
	Material _pantMaterial = null;
	Material _gloveMaterial = null;
	Material _hatMaterial = null;
	Material _shirtMaterial = null;
	Material _coutMaterial = null;


	// Use this for initialization
	void Start()
	{
		_hatMaterial = HatMaterial;
	}

	// Update is called once per frame
	void Update()
	{
		showCout = ShowCout;
		showGloves = ShowGloves;
		showHat = ShowHat;

		if (_pantMaterial != PantMaterial)
		{
			_pantMaterial = PantMaterial;
			SetPantMaterial();
		}
		if (_hatMaterial != HatMaterial)
		{
			_hatMaterial = HatMaterial;
			SetHatMaterial();
		}
		if (_shirtMaterial != ShirtMaterial )
		{
			_shirtMaterial = ShirtMaterial;
			SetShirtMaterial();
		}
		if (_coutMaterial != CoutMaterial)
		{
			_coutMaterial = CoutMaterial;
			if (ShowCout)
			{
				_bodyMaterial = CoutMaterial;
				SetBodyMaterial();
			}
		}
	}

	public void SetShirtMaterial()
	{
		if (Shirt) Shirt.GetComponent<Renderer>().sharedMaterial = _shirtMaterial;
		if (!ShowCout)
		{
			_bodyMaterial = _shirtMaterial;
			SetBodyMaterial();
		}
	}

	public void SetBodyMaterial()
	{
		if (_showCout && Cout) Cout.GetComponent<Renderer>().sharedMaterial = _bodyMaterial;
		if (Shoulders) Shoulders.GetComponent<Renderer>().sharedMaterial = _bodyMaterial;
		if (HandLeft) HandLeft.GetComponent<Renderer>().sharedMaterial = _bodyMaterial;
		if (HaneRight) HaneRight.GetComponent<Renderer>().sharedMaterial = _bodyMaterial;
	}
	public void SetPantMaterial()
	{
		if (Hip) Hip.GetComponent<Renderer>().sharedMaterial = _pantMaterial;
		if (LegLeft) LegLeft.GetComponent<Renderer>().sharedMaterial = _pantMaterial;
		if (LegRight) LegRight.GetComponent<Renderer>().sharedMaterial = _pantMaterial;
	}
	public void SetGloweMaterial()
	{
		if (GloveLeft) GloveLeft.GetComponent<Renderer>().sharedMaterial = _gloveMaterial;
		if (GloveRight) GloveRight.GetComponent<Renderer>().sharedMaterial = _gloveMaterial;
	}
	public void SetHatMaterial()
	{
		if (Hat) Hat.GetComponent<Renderer>().sharedMaterial = _hatMaterial;
		if (HatCilinder) HatCilinder.GetComponent<Renderer>().sharedMaterial = _hatMaterial;
	}
}
