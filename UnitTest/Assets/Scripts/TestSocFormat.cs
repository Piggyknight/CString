using System;
using System.IO;
using UnityEngine;

public class TestSocFormat : MonoBehaviour
{
    private FileStream fs;
    public void Start()
    {
        fs = new FileStream("D:\\test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 100000, FileOptions.None);
    }

    public void OnDisable()
    {
        fs?.Dispose(); 
    }

    public void Update()
    {
        int a = 1;
        int b = 2;
        int c = 3;
        int d = 4;
        CString cs = SocString.Format("{0} {1} {2} {3}", a, b, c, d);
        for(int i=0; i<cs.length; i++)
        {
            fs.WriteByte((byte)cs[i]);
        }
    }    
}
