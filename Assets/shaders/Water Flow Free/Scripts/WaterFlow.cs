// Water Flow FREE version: 1.0.2
// Author: Gold Experience Team (http://www.ge-team.com/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion

/***************
* WaterFlow class
* This class animates UV offset
**************/

public class WaterFlow : MonoBehaviour {
	
	#region Variables
	
		// UV speed
	public float m_SpeedU = 0.1f;
	public float m_SpeedV = -0.1f;

	#endregion

	// ######################################################################
	// MonoBehaviour Functions
	// ######################################################################

	#region Component Segments
	float randomTime = 3f;
	float dir = 1;
	float dirT = -1;
	// Update is called once per frame
	void Update () {

		randomTime -= Time.deltaTime;
		if (randomTime <= 0) {
			randomTime = Random.Range(2f, 4f);
			dir *= -1;
			dirT = 0;
		}
		dirT = Mathf.Lerp(dirT, dir, 0.001f);

		// Update new UV speed
		float newOffsetU = Time.time * m_SpeedU;
		float newOffsetV = Time.time * m_SpeedV * dirT;
		
		// Check if there is renderer component
		if (this.GetComponent<Renderer>())
		{
			// Update main texture offset
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(newOffsetU, newOffsetV);
		}
	}
	
	#endregion {Component Segments}
}