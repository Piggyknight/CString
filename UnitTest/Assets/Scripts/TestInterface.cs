using System;
using UnityEngine;

public interface TestInteface
{
    void Test();
}

public class TestA : TestInteface
{
    public void Test()
    {
        Debug.Log("TestA");
    }
}

public class TestB : TestA
{
    public new void Test()
    {
        Debug.Log("TestB");
    }
}

public class TestInterface : MonoBehaviour
{
    public void Start()
    {
        TestB B1 = new TestB();
        B1.Test();

        TestA A1 = B1;
        A1.Test();

        TestInteface I1 = B1;
        I1.Test();
    }

    public void Update()
    {
        
    }
}
