using System;
using System.IO;
using Newtonsoft.Json;

public static class SaveLoadSystem
{
    public static void Save<T>(T data, string path)
    {
        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(jsonWriter, data);
            }
        }
    }
    public static T Load<T>(string path)
    {
        if (File.Exists(path) == false) Save((T)Activator.CreateInstance(typeof(T)), path);
        T result;

        using (StreamReader streamReader = new StreamReader(path))
        {
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                result = (T)jsonSerializer.Deserialize(jsonReader, typeof(T));
            }
        }
        return result;
    }
}