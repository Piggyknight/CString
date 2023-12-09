using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;
using System.Text;
//using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // public Button buttonA;
    //

    public StringBuilder _sb = new StringBuilder(10000);

    public void Update()
    {
        int COUNT = 1000;
        List<string> strings = new List<string>();
        for (int i = 0; i < 100; i++)
            strings.Add("123456789");

        // string ret = ZString.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        // _sb.Append(ret);
        //
        // Profiler.BeginSample("Append/ZString.StringBuilder()");
        // for (int i = 0; i < COUNT; i++)
        // {
        //     using (var sb = ZString.CreateStringBuilder(true))
        //     {
        //         for (int j = 0; j < strings.Count; j++)
        //         {
        //             sb.Append(strings[j]);
        //         }
        //
        //         sb.ToString();
        //     }
        // }
        // Profiler.EndSample();


        Profiler.BeginSample("Append/SharedStringBuilderScope()");
        {
            for (int i = 0; i < COUNT; i++)
            {
                _sb.Clear();
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int j = 0; j < strings.Count; j++)
                {
                    _sb.Append(strings[j]);
                }
        
                _sb.ToString();
                // sb.Clear();
            }
        }
        Profiler.EndSample();
    }
}