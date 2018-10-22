using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour
{
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }
}
