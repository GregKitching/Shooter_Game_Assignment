using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class JsonSampleData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SampleData sampleData = new SampleData();
        sampleData.name = "AAA";
        sampleData.address = new Address();
        sampleData.address.unit = 5;
        sampleData.address.street = "BBB";
        sampleData.address.city = "CCC";
        sampleData.books = new Book[2];
        sampleData.books[0] = new Book();
        sampleData.books[0].name = "Book1";
        sampleData.books[0].isDigital = false;
        sampleData.books[1] = new Book();
        sampleData.books[1].name = "Book2";
        sampleData.books[1].isDigital = true;

        string data = JsonUtility.ToJson(sampleData,true);
        Debug.Log(data);

        string path = Path.Combine(Application.dataPath, "Scripts/JSONINFO/sampleData.JSON.json");
        File.WriteAllText(path, data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
