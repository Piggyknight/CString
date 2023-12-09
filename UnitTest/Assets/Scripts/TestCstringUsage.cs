using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestCstringUsage : MonoBehaviour
    {
        public int val_int = 123;
        public float val_float = 0.888f;
        public double val_double = 0.999;
        public long val_long = 1234567890;
        public string val_string = "test";
        
        public CString str = CString.Alloc(256);
        public void TestNumberFormater()
        {
            str.Append(val_int);
            str.Append(val_float);
            str.Append(val_double);
            str.Append(val_long);
            str.Append(val_string);
            str.Clear();
            
            
            //Debug.Log(str.ToString());
        }


        public void Update()
        {
            TestNumberFormater();
        }
    }
}