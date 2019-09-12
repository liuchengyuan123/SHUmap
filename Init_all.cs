public GameObject obj;
    public GameObject sign;
    public int p_tot;
    public GameObject[] fjw = new GameObject[250];
	// Use this for initialization
	void Start () {
        p_tot = 0;
        read_from_file();
        Debug.Log(p_tot);
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
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitt = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitt);
            Vector3 pos = hitt.point;
            int id = find_closest(pos);
            //Instantiate(sign, fjw[id], Quaternion.identity);
            Debug.Log(fjw[id].transform.position);
        }
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
