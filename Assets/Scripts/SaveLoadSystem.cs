using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Xml.Serialization;

public enum SerializeType
{
    JSON,
    XML,
    BinaryFormatter,
}

public static class SaveLoadSystem
{
    public static void Save<T>(T data, string path, SerializeType serializeType)
    {
        switch (serializeType)
        {
            case SerializeType.JSON:
                using (StreamWriter streamWriter = new StreamWriter(path + ".json"))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        jsonSerializer.Serialize(jsonWriter, data);
                    }
                }
                break;
            case SerializeType.XML:
                using (TextWriter textWriter = new StreamWriter(path + ".xml"))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(textWriter, data);
                }
                break;
            case SerializeType.BinaryFormatter:
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, data);
                }
                break;
        }
        
    }
    public static T Load<T>(string path, SerializeType serializeType)
    {
        if (File.Exists(path) == false) return default(T);
        T result = default(T);
        switch (serializeType)
        {
            case SerializeType.JSON:
                using (StreamReader streamWriter = new StreamReader(path + ".json"))
                {
                    using (JsonReader jsonReader = new JsonTextReader(streamWriter))
                    {
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        result = (T)jsonSerializer.Deserialize(jsonReader, typeof(T));
                    }
                }
                break;
            case SerializeType.XML:
                using (TextReader textReader = new StreamReader(path + ".xml"))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = (T)xmlSerializer.Deserialize(textReader);
                }
                break;
            case SerializeType.BinaryFormatter:
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    result = (T)binaryFormatter.Deserialize(fileStream);
                }
                break;
        }
        return result;
    }
}