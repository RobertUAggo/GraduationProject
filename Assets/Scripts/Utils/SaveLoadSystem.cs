using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System;

public enum SerializeType
{
    JSON,
    XML,
    BinaryFormatter,
    BinaryFormatterWithEncryption,
}
public static class SaveLoadSystem
{
    //const string cryptoKey = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";
    const string cryptoKey = "YnJlYXRoZWV4YWN0bHl0dXJubWVucmF0aGVybW9udGg=";
    private const int keySize = 256;
    private const int ivSize = 16;
    private static readonly byte[] key = Convert.FromBase64String(cryptoKey);
    public static void Save<T>(T data, string path, SerializeType serializeType)
    {
        switch (serializeType)
        {
            case SerializeType.JSON:
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        jsonSerializer.Serialize(jsonWriter, data);
                    }
                }
                break;
            case SerializeType.XML:
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(textWriter, data);
                }
                break;
            case SerializeType.BinaryFormatter:
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, data);
                }
                break;
            case SerializeType.BinaryFormatterWithEncryption:
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    using (CryptoStream cryptoStream = CreateEncryptionStream(key, fileStream))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        binaryFormatter.Serialize(cryptoStream, data);
                    }
                }
                break;
        }
    }
    public static T Load<T>(string path, SerializeType serializeType)
    {
        if (File.Exists(path) == false) Save((T)Activator.CreateInstance(typeof(T)), path, serializeType);
        T result;
        switch (serializeType)
        {
            case SerializeType.JSON:
                using (StreamReader streamReader = new StreamReader(path))
                {
                    using (JsonReader jsonReader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        result = (T)jsonSerializer.Deserialize(jsonReader, typeof(T));
                    }
                }
                break;
            case SerializeType.XML:
                using (TextReader textReader = new StreamReader(path))
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
            case SerializeType.BinaryFormatterWithEncryption:
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (CryptoStream cryptoStream = CreateDecryptionStream(key, fileStream))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        result = (T)binaryFormatter.Deserialize(cryptoStream);
                    }
                }
                break;
            default:
                result = default(T);
                break;
        }
        return result;
    }

   public static CryptoStream CreateEncryptionStream(byte[] key, Stream outputStream)
   {
       byte[] iv = new byte[ivSize];

       using (var rng = new RNGCryptoServiceProvider())
       {
           // Using a cryptographic random number generator
           rng.GetNonZeroBytes(iv);
       }

       // Write IV to the start of the stream
       outputStream.Write(iv, 0, iv.Length);

       Rijndael rijndael = new RijndaelManaged();
       rijndael.KeySize = keySize;

       CryptoStream encryptor = new CryptoStream(
           outputStream,
           rijndael.CreateEncryptor(key, iv),
           CryptoStreamMode.Write);
       return encryptor;
   }

   public static CryptoStream CreateDecryptionStream(byte[] key, Stream inputStream)
   {
       byte[] iv = new byte[ivSize];

       if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
       {
           throw new Exception("Failed to read IV from stream.");
       }

       Rijndael rijndael = new RijndaelManaged();
       rijndael.KeySize = keySize;

       CryptoStream decryptor = new CryptoStream(
           inputStream,
           rijndael.CreateDecryptor(key, iv),
           CryptoStreamMode.Read);
       return decryptor;
   }
}