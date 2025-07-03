using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int limiteSpawn = 10;
    public float tiempoEntreSpawns = 0.5f;
    public Vector2 areaSpawnMin;
    public Vector2 areaSpawnMax;

    public float tiempoDesaparecerMin = 2f;
    public float tiempoDesaparecerMax = 5f;

    private int spawnsRealizados = 0;

    void Start()
    {
        StartCoroutine(AparecerObjetivos());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Objetivo"))
                {
                    Destroy(hit.collider.gameObject);
                    spawnsRealizados--;
                }
            }
        }
    }

    private IEnumerator AparecerObjetivos()
    {
        while (true) 
        {
            if(spawnsRealizados <= limiteSpawn)
            {
                float spawnX = Random.Range(areaSpawnMin.x, areaSpawnMax.x);
                float spawnY = Random.Range(areaSpawnMin.y, areaSpawnMax.y);
                Vector3 posicionAleatoria = new Vector3(spawnX, spawnY, 0);


                GameObject nuevoObjetivo = Instantiate(prefab, posicionAleatoria, Quaternion.identity);
                nuevoObjetivo.tag = "Objetivo";

                float tiempoDesaparecer = Random.Range(tiempoDesaparecerMin, tiempoDesaparecerMax);
                Destroy(nuevoObjetivo, tiempoDesaparecer);

                spawnsRealizados++;
            }

            yield return new WaitForSeconds(tiempoEntreSpawns);

        }
        
    }

}
