using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class WorkWithFile
{
    public static void WriteJSON<T>(T tJsonObject, string stringPath)
    {
        string str = JsonUtility.ToJson(tJsonObject, true);

        //str = Secure.Encode(ref str);

        using (var fileJSON = new FileStream(stringPath, FileMode.Create, FileAccess.Write))
        {
            StreamWriter writer = new StreamWriter(fileJSON);
            writer.Write(str);
            writer.Flush();
        }
    }

    public static T ReadJSON<T>(string stringPath)
    {
        string str;

        using (var fileJSON = new FileStream(stringPath, FileMode.Open, FileAccess.Read))
        {
            StreamReader reader = new StreamReader(fileJSON);
            str = reader.ReadToEnd();
        }

        //str = Secure.Decode(ref str);

        T tJsonContent = JsonUtility.FromJson<T>(str);

        return tJsonContent;
    }

    public static bool IsFileEmpty(string path)
    {
        using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            StreamReader streamReader = new StreamReader(file);
            string str = streamReader.ReadToEnd();

            if (str == "")
            {
                return true;
            }
        }

        return false;
    }
}
