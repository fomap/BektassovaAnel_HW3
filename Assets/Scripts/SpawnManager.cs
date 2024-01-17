using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
   public GameObject cube;
   [SerializeField] public GameObject[] arr = new GameObject[10];
   [SerializeField] public GameObject[] targets = new GameObject[10];
   
   void Start()
   {
        for(int i = 0; i < 10; i++)
        { 
         Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 3, Random.Range(-10.0f, 10.0f));
         arr[i] = Instantiate(cube,pos,Quaternion.identity);
         arr[i].transform.localScale =  new Vector3(Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f));
         arr[i].GetComponent<MeshRenderer>().material.color = new Color(Random.Range( 0.0f, 1.0f ) , Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ), 1 );
        }
   }

    private void Move(int i)
    {
        Vector3 tmp = new Vector3(0, arr[i].transform.position.y, targets[i].transform.position.z);
        while(arr[i].transform.position != tmp)
        {
            arr[i].transform.position = Vector3.MoveTowards(arr[i].transform.position, tmp, 0.03f); 
        }
    }

    private void VolumeSort()
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
    }

    private void HueSort()
    {
        for(int i = 0; i < 9; i++)
        {
            float H1, S1, V1;
            Color.RGBToHSV(arr[i].GetComponent<MeshRenderer>().material.color, out H1, out S1, out V1);
            
            float minHue = H1;  
            int minindex = i;
            for(int j = i+1; j < 10; j++) 
            {
                float H2, S2, V2;
                Color.RGBToHSV(arr[j].GetComponent<MeshRenderer>().material.color, out H2, out S2, out V2);

                if(H2 < minHue)
                {
                    minHue = H2;
                    minindex = j;
                }
            }

            var tempVar = arr[minindex];
            arr[minindex] = arr[i];
            arr[i] = tempVar;
        }
    }

    public void SortBtn()
    {
        VolumeSort();
        StartCoroutine(MyCoroutine());
    }

    public void HueSortBtn()
    {
        HueSort();
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine()
    {
        for(int i = 0; i < 10; i++)
        {
            Move(i);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Shuffle()
    {
        SceneManager.LoadScene(0);
    }

}



