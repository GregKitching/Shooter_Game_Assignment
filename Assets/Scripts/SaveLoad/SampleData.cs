using System;

[Serializable]
public class Address {
    public int unit;
    public string street;
    public string city;
}

[Serializable]
public class Book {
    public string name;
    public bool isDigital;
}

[Serializable]
public class SampleData
{
    public string name;
    public Address address;
    public Book[] books;
}
