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
        Console.WriteLine("TestA");
    }
}

public class TestB : TestA
{
    public new void Test()
    {
        Console.WriteLine("TestB");
    }
}

public class TestInterface : MonoBehaviour
{
    public void Start()
    {
        TestB B = new TestB();
        B.Test();
    }

    public void Update()
    {
            
    }
}
