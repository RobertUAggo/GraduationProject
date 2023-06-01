using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System;

public static class SaveLoadSystem
{
    //const string cryptoKey = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";
    const string cryptoKey = "YnJlYXRoZWV4YWN0bHl0dXJubWVucmF0aGVybW9udGg=";
    private const int keySize = 256;
    private const int ivSize = 16;
    private static readonly byte[] key = Convert.FromBase64String(cryptoKey);
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