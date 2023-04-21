using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveFile
{
    private string _fileName;
    private IFormatter _formatter;
    private string _fullPath;
    public SaveFile(string fileName, IFormatter formatter)
    {
        _fileName = fileName;
        _formatter = formatter;
        _fullPath = Path.Combine(Application.persistentDataPath, _fileName);
    }
    public void Save(object data)
    {
        try
        {
            using (FileStream fs = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    _formatter.Serialize(sw.BaseStream, data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[!!!] Error while saving\n" + e.Message);
        }
    }
    public object Load()
    {
        if (File.Exists(_fullPath) == false) return null;

        object result = null;
        try
        {
            using (FileStream fs = new FileStream(_fullPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    result = _formatter.Deserialize(sr.BaseStream);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[!!!] Error while saving\n" + e.Message);
        }
        return result;
    }
}