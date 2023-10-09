using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class BigNumber
{
    public static string Abbreviate(int num)
    {
        return Abbreviate(num, true);
    }

    public static string Abbreviate(string stNum)
    {
        int num;

        if (int.TryParse(stNum, out num) == false)
        {
            Debug.LogError("Not a number, can not abbreviate.");
            return null;
        }

        return Abbreviate(num);
    }
    
    public static string Abbreviate(string stNum, bool dot)
    {
        int num;

        if (int.TryParse(stNum, out num) == false)
        {
            Debug.LogError("Not a number, can not abbreviate.");
            return null;
        }

        return Abbreviate(num, dot);
    }

    public static string Abbreviate(int num, bool dot)
    {
        string stNum = "";

        if (num >= 1000000000)
        {
            int a = num / 1000000000;
            int b = num % 1000000000;
            b = b / 100000000;
            stNum = a.ToString() + (b == 0 ? "" : (dot == true ? "." : ",") + b.ToString()) + " B";
        }
        else if (num >= 1000000)
        {
            int a = num / 1000000;
            int b = num % 1000000;
            b = b / 100000;
            stNum = a.ToString() + (b == 0 ? "" : (dot == true ? "." : ",") + b.ToString()) + " M";
        }
        else if (num >= 1000)
        {
            int a = num / 1000;
            int b = num % 1000;
            b = b / 100;
            stNum = a.ToString() + (b == 0 ? "" : (dot == true ? "." : ",") + b.ToString()) + " K";
        }
        else
        {
            stNum = num.ToString();
        }

        return stNum;
    }
}
