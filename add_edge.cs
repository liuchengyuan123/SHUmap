using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Init_all : MonoBehaviour {

    public GameObject obj;
    private int last;
    private int p_tot;
    private GameObject[] fjw = new GameObject[250];
    private float[,] edge = new float[250, 250];
    private bool check_mod;
    // Use this for initialization
    void Start()
    {
        p_tot = 0;
        read_from_file();
        Debug.Log(p_tot);
        last = 0;
        check_mod = false;
        for (int i = 1; i <= 213; i++)
        {
            for (int j = 1; j <= 213; j++)
            {
                edge[i,j] = -1f;
            }
        }
    }

    float distance(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + 
            (p1.y - p2.y) * (p1.y - p2.y) + (p1.z - p2.z) * (p1.z - p2.z));
    }

    int find_closest(Vector3 loc)
    {
        int ans = 1;
        for (int i = 2; i <= p_tot; i++)
        {
            if (distance(fjw[ans].transform.position, loc) > distance(fjw[i].transform.position, loc))
                ans = i;
        }
        return ans;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            check_mod = true;
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitt = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitt);
            Vector3 pos = hitt.point;
            int id = find_closest(pos);
            //Instantiate(sign, fjw[id], Quaternion.identity);
            Debug.Log(id);
            Debug.Log("position");
            Debug.Log(fjw[id].transform.position);
            Debug.Log("this last");
            Debug.Log(last);
            if (check_mod)
            {
                for (int i = 1; i <= 250; i++)
                    if (edge[id, i] != -1)
                        Debug.Log(i);
                check_mod = false;
            }
            else
            {
                if (last == 0)
                    last = id;
                else
                {
                    Debug.Log("id");
                    Debug.Log(id);
                    Debug.Log("last");
                    Debug.Log(last);
                    float x = distance(fjw[id].transform.position, fjw[last].transform.position);
                    Debug.Log(x);
                    edge[id, last] = x;
                    edge[last, id] = x;
                    last = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 1; i <= 213; i++)
                for (int j = i; j <= 213; j++)
                    if (edge[i, j] != -1)
                    {
                        string msg = string.Format("{0:D},{1:D},{2:F}", i, j, edge[i, j]);
                        write(msg);
                    }
        }
	}

    void write(string msg)
    {
        FileInfo t = new FileInfo("E:\\comp\\Unity\\New Unity Project\\Assets\\Scenes\\edgedata.txt");
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
            
            fjw[++p_tot] = Instantiate(obj, p, Quaternion.identity);
            Debug.Log(p_tot);
        }
        sr.Close();
        sr.Dispose();
    }
}
