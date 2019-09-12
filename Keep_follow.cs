using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keep_follow : MonoBehaviour {

    public GameObject tar;
    public GameObject self;
    public Vector3 dis;

	// Use this for initialization
	void Start () {
        Vector3 v1 = self.transform.position;
        Vector3 v2 = tar.transform.position;
        dis = new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tar_p = tar.transform.position;
        Vector3 show = new Vector3(dis.x + tar_p.x, dis.y + tar_p.y, dis.z + tar_p.z);
        self.transform.position = show;
	}
}
