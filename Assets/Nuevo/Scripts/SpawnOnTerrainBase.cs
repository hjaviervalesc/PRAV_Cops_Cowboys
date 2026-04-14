using UnityEngine;

//Clase base para objetos que deban spawnearse sobre el terreno
public abstract class SpawnOnTerrainBase : MonoBehaviour
{
    //Capa que define que es suelo
    [SerializeField] protected LayerMask groundMask;

    //Devuelve un punto sobre el terreno usando un raycast hacia abajo
    protected Vector3 GetPointAboveTerrain(Vector3 randomXZ, float height = 50f)
    {
        Vector3 origin = new Vector3(randomXZ.x, height, randomXZ.z);

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 200f, groundMask))
        {
            return hit.point; //Punto exacto del suelo
        }

        return randomXZ; //Si no golpea nada, devuelve la posición original
    }

    //Genera una posición aleatoria en XZ
    protected Vector3 GetRandomXZ(float minX, float maxX, float minZ, float maxZ)
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, 0, z);
    }
}