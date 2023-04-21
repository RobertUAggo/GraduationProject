using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveFile<T>
{
    private string _fileName;
    private IFormatter _formatter = new BinaryFormatter();
    private string _fullPath;
    public SaveFile(string fileName)
    {
        _fileName = fileName;
        _fullPath = Path.Combine(Application.persistentDataPath, _fileName);
    }
    public void Save(T data)
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
    public T Load()
    {
        if (File.Exists(_fullPath) == false) return default(T);

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
        return (T) result;
    }
}