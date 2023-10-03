using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secure : MonoBehaviour
{
    static char[] arrSecureChar = new char[60] {'Q', '[', 't', 'V', 'M', 'g', 'v', 'u', 'h', 'T', 
                                        'm', ']', 's', 'I', 'Z', '{', 'E', 'C', 'K', 'L', 
                                        ',', 'x', 'U', 'H', ':', 'e', 'k', 'R', 'N', 'z', 
                                        'b', 'W', 'p', 'j', 'c', 'B', 'Y', 'y', 'J', 'O', 
                                        'a', 'd', '}', 'i', 'l', 'r', 'f', 'A', 'q', 'P', 
                                        '"', 'X', 'o', 'F', 'G', 'w', 'D', 'S', 'n', ' '};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Create Arr Char Secure
    //private void CreateArrSecure(char[] arrSecureChar)
    //{
    //    int i = 0;
    //    char a; 
        
    //    a = 'A';
    //    for (; i < 26;)
    //    {
    //        arrSecureChar[i] = a;
    //        a++;
    //        i++;
    //    }

    //    a = 'a';
    //    for (; i < 52;)
    //    {
    //        arrSecureChar[i] = a;
    //        a++;
    //        i++;
    //    }

    //    arrSecureChar[52] = '"';
    //    arrSecureChar[53] = '[';
    //    arrSecureChar[54] = ']';
    //    arrSecureChar[55] = '{';
    //    arrSecureChar[56] = '}';
    //    arrSecureChar[57] = ',';
    //    arrSecureChar[58] = ':';
    //    arrSecureChar[58] = ' ';

    //    Shuffle(arrSecureChar);
    //}

    //private void PrintArrSecure(char[] arrSecureChar)
    //{
    //    string str = "";
        
    //    for(int i = 0; i < arrSecureChar.Length; i++)
    //    {
    //        str += '\'' + arrSecureChar[i].ToString() + '\'' + ',';
    //    } 

    //    Debug.Log(str);
    //}

    //private void Shuffle(char[] arrSecureChar)
    //{
    //    int i = arrSecureChar.Length - 1;

    //    int rd;

    //    while (i >= 0) 
    //    {
    //        rd = Random.Range(0, arrSecureChar.Length);

    //        if (rd != i)
    //        {
    //            Swap<char>(ref arrSecureChar[i], ref arrSecureChar[rd]);
    //        }
    //        else
    //        {
    //            continue;
    //        }

    //        i--;
    //    }
    //}

    //private void Swap<T>(ref T char_1, ref T char_2)
    //{
    //    T temp = char_1;
    //    char_1 = char_2;
    //    char_2 = temp;
    //}
    #endregion
    public static string Encode(ref string info)
    {
        char[] arrCharInfo = info.ToCharArray();

        for (int i = 0; i < arrCharInfo.Length; i++)
        {
            for (int j = 0; j < arrSecureChar.Length; j++)
            {
                if (arrCharInfo[i] == arrSecureChar[j])
                {
                    arrCharInfo[i] = arrSecureChar[j + SecureConstant.SECURE_KEY < arrSecureChar.Length 
                        ? j + SecureConstant.SECURE_KEY : j + SecureConstant.SECURE_KEY - arrSecureChar.Length];
                    
                    break;
                }
            } 
        }

        info = new string(arrCharInfo);

        return info;
    }

    public static string Decode(ref string info)
    {
        char[] arrCharInfo = info.ToCharArray();

        for (int i = 0; i < arrCharInfo.Length; i++)
        {
            for (int j = 0; j < arrSecureChar.Length; j++)
            {
                if (arrCharInfo[i] == arrSecureChar[j])
                {
                    arrCharInfo[i] = arrSecureChar[j - SecureConstant.SECURE_KEY >= 0
                        ? j - SecureConstant.SECURE_KEY : arrSecureChar.Length + (j - SecureConstant.SECURE_KEY)];

                    break;
                }
            }
        }

        info = new string(arrCharInfo);

        return info;
    }
}

public class SecureConstant
{
    internal const int SECURE_KEY = 10;
}