using UnityEngine;
using System.Collections.Generic;

public class TransformsEnemy : MonoBehaviour
{
    public Transform[] transforms;
    private List<int> usedIndices = new List<int>();
    private int sumPorts;

    public Transform TEnemy()
    {
        if (transforms != null)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, transforms.Length);
            }
            while (usedIndices.Contains(randomIndex) && usedIndices.Count < transforms.Length); 
            sumPorts++;
            usedIndices.Add(randomIndex);
            return transforms[randomIndex];
        }
        return null; 
    }
}
