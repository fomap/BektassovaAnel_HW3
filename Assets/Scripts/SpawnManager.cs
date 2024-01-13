using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
   public GameObject cube;

   [SerializeField] private GameObject[] arr = new GameObject[10];
   [SerializeField] private GameObject[] targets = new GameObject[10];
   
   void Start()
   {
        for(int i = 0; i < 10; i++)
        {
         Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 3, Random.Range(-10.0f, 10.0f));
         arr[i] = Instantiate(cube,pos,Quaternion.identity);
         arr[i].transform.localScale =  new Vector3(Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f));
        }

   }
    void Update()
    {
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 8; j++)
            {
                float jvolume = arr[j].transform.localScale.x * arr[j].transform.localScale.y * arr[j].transform.localScale.z;
                float j1volume =  arr[j+1].transform.localScale.x * arr[j+1].transform.localScale.y * arr[j+1].transform.localScale.z;
                if (jvolume > j1volume)
                {
                    var tempVar = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tempVar;
                }
            }
                
        }

        for(int i = 0; i < 10; i++)
        {
            Move(i);
        }
    }

    private void Move(int i)
    {
        Vector3 tmp = new Vector3(0, arr[i].transform.position.y, targets[i].transform.position.z);
        arr[i].transform.position = Vector3.MoveTowards(arr[i].transform.position, tmp, 0.03f);
    }
}



