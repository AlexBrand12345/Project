using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.gd");
        bf.Serialize(file, MainSave.save);
        file.Close();
    }

    public static bool Load()
    {
        if(File.Exists(Application.persistentDataPath + "/save.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.gd", FileMode.Open);
            MainSave.save = (MainSave)bf.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
}
