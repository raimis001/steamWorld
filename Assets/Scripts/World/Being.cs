using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using UnityEngine;

public class Being : MonoBehaviour
{

    private float tiredness = 0;
    private float hunger = 0;
    private float fear = 0;
    private float thirst = 0;
	

	void Update ()
	{
	    tiredness += 0.01f;
	    hunger += 0.01f;
	    thirst += 0.02f;

	    if (hunger > 0.5f && tiredness > 0.5f)
	    {
	        fear += 0.01f;
	    }
	}

    public float Fear
    {
        get { return fear; }
        set { fear = value; }
    }

    public float Tiredness
    {
        get { return tiredness;}
        set { tiredness = value; }
    }

    public float Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public float Thirst
    {
        get { return thirst; }
        set { thirst = value; }
    }
}
