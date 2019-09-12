using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class get_mouse_pos : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
        read_from_file();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitt = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitt);
            Vector3 pos = hitt.point;
            Debug.Log(pos);
            Instantiate(obj, pos, Quaternion.identity);
            string msg = string.Format("{0:F},{1:F},{2:F}", pos.x, pos.y, pos.z);
            write(msg);
        }
    }

    void write(string msg)
    {
        FileInfo t = new FileInfo("E:\\comp\\Unity\\New Unity Project\\Assets\\Scenes\\pointdata.txt");
        StreamWriter sw = t.AppendText();
        sw.WriteLine(msg);
        sw.Close();
        sw.Dispose();
    }

    void read_from_file()
    {
        FileInfo t = new FileInfo("E:\\comp\\Unity\\New Unity Project\\Assets\\Scenes\\pointdata.txt");
        if (!t.Exists)
        {
            return;
        }
        StreamReader sr = null;
        sr = File.OpenText("E:\\comp\\Unity\\New Unity Project\\Assets\\Scenes\\pointdata.txt");
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] msg = line.Split(',');
            Vector3 p;
            p.x = float.Parse(msg[0]);
            p.y = float.Parse(msg[1]);
            p.z = float.Parse(msg[2]);
            Debug.Log(p);
            Instantiate(obj, p, Quaternion.identity);
        }
        sr.Close();
        sr.Dispose();
    }
}
