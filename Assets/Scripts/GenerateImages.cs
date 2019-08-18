using UnityEngine;
using System.Collections;

public class GenerateImages : MonoBehaviour
{
    private Object[] textures;
    public GameObject thumb;
    public float velocity = 1;
    private GameObject[] values;


    void Start()
    {

        textures = Resources.LoadAll("Icons", typeof(Texture2D));

        values = new GameObject[textures.Length];
        StartCoroutine(CreateImages());

    }

    void Update()
    {

        float minimalVelocity = 0.01f;
        if (values.Length > 0)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i])
                {
                    if (values[i].GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
                    {
                        values[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1 * velocity, velocity), Random.Range(-1 * velocity, velocity), 0);
                    }

                    if ( values[i].GetComponent<Rigidbody>().velocity.x < minimalVelocity && values[i].GetComponent<Rigidbody>().velocity.x > minimalVelocity *-1)
                    {
                        values[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1 * velocity, velocity), values[i].GetComponent<Rigidbody>().velocity.y, 0);
                    }

                    if (values[i].GetComponent<Rigidbody>().velocity.y < minimalVelocity && values[i].GetComponent<Rigidbody>().velocity.y > minimalVelocity *-1)
                    {
                        values[i].GetComponent<Rigidbody>().velocity = new Vector3(values[i].GetComponent<Rigidbody>().velocity.x, Random.Range(-1 * velocity, velocity), 0);
                    }
                }
            }
        }

    }

    private IEnumerator CreateImages()
    {

        for (int i = 0; i < textures.Length; i++)
        {
            values[i] = Instantiate(thumb);
            values[i].name = "Image_" + i;

            values[i].transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), -1+ 0.01f *i);
            Texture2D texture = (Texture2D)textures[i];

            values[i].GetComponent<Renderer>().material.mainTexture = texture;

            float randomx = Random.Range(-1 * velocity, velocity);
            float randomy = Random.Range(-1 * velocity, velocity);
            values[i].GetComponent<Rigidbody>().velocity = new Vector3(randomx, randomy, 0);

            //ignore other pieces

            for (int x = 0; x < values.Length; x++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[x] && values[j])
                    {
                        Physics.IgnoreCollision(values[x].GetComponent<Collider>(), values[j].GetComponent<Collider>());
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
      
    }




}