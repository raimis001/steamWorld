using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ObjectStage { PLACE, BUILD, CHOP, GROW, RIPE, READY }

public class objMain : MonoBehaviour {

	public bool ArrowDamage = false;
	public bool IgnoreCollider = false;

	ObjectStage _stage = ObjectStage.PLACE;
	public ObjectStage Stage {
		get { return _stage; }
		set {

			ObjectStage old = _stage;
			_stage = value;

			ChangeStage(old);
		}
	}


	// Use this for initialization
	virtual protected void Start() {
		if (IgnoreCollider) PlayerControl.Ignore(gameObject);
	}
	
	// Update is called once per frame
	virtual protected void Update () {
	
	}

	virtual protected void ChangeStage(ObjectStage oldStage) {
	}

	void OnMouseUp()
	{
		MainGUI.Instance.SetMainIcon(this);
		Interaction();
	}

	public virtual void Interaction()
	{
	}

	public virtual bool DoShot(ItemNames itemID) {
		return ArrowDamage;
	}
}
