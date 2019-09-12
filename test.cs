using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class test : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character;
        private bool moving;
        Vector3 dir;
        public GameObject obj;
        int p_tot;
        GameObject[] fjw = new GameObject[250];
        // Use this for initialization
        void Start()
        {
            m_Character = FindObjectOfType<ThirdPersonCharacter>();
            p_tot = 0;
            read_from_file();
            Debug.Log(p_tot);
            dir = m_Character.transform.position;
            moving = false;
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
        void Update()
        {
            /*
            Vector3 cur = m_Character.transform.position;
            Debug.Log(cur);
            if (distance(cur, new Vector3(0.0f, 0.0f, 0.0f)) > 0.1f)
            {
                dir = cur / distance(cur, new Vector3(0.0f, 0.0f, 0.0f));
                m_Character.Move(-dir * 2f, false, false);
            }
            */
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitt = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hitt);
                Vector3 pos = hitt.point;
                int id = find_closest(pos);
                //Instantiate(sign, fjw[id], Quaternion.identity);
                Debug.Log(fjw[id].transform.position);
                dir = fjw[id].transform.position;
                moving = true;
            }
            Vector3 cur = m_Character.transform.position;
            if (moving && distance(dir, cur) > 0.1f)
            {
                Vector3 to = dir - cur;
                to /= distance(to, new Vector3(0f, 0f, 0f));
                Vector3 sp = to * 2.5f;
                m_Character.Move(sp, false, false);
            }
            else
                moving = false;
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
}
