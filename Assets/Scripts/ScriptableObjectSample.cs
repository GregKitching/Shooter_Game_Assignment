using UnityEngine;

[CreateAssetMenu(fileName = "Data Sample", menuName = "Something/aaa", order = 1)]
public class ScriptableObjectSample : ScriptableObject
{
    public string objectName;
    public int score;
}
