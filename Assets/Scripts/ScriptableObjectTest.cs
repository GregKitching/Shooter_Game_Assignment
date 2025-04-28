using UnityEngine;

public class ScriptableObjectTest : MonoBehaviour
{
    public ScriptableObjectSample sample;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!sample) {
            return;
        }
        Debug.Log(sample.objectName + ", " + sample.score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
