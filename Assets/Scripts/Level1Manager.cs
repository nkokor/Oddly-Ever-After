using UnityEngine;

public class Leve1lManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject objectPrefab;
    
    // Matrice za različite slojeve
    private char[,] layer1 = {
        { 'W', 'W', 'W', 'W', 'W' },
        { 'W', 'P', 'P', 'P', 'W' },
        { 'W', 'P', 'E', 'P', 'W' },
        { 'W', 'P', 'P', 'P', 'W' },
        { 'W', 'W', 'W', 'W', 'W' }
    };

    private char[,] layer2 = {
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' }
    };

    private char[,] layer3 = {
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', 'O', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' }
    };

    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        float tileSize = 1f;

        for (int i = 0; i < layer1.GetLength(0); i++)
        {
            for (int j = 0; j < layer1.GetLength(1); j++)
            {
                Vector3 position = new Vector3(j * tileSize, 0, i * tileSize);

                // Postavi objekte za sloj 1 (npr. zidovi)
                if (layer1[i, j] == 'W')
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (layer1[i, j] == 'P')
                {
                    Instantiate(pathPrefab, position, Quaternion.identity);
                }

                // Postavi objekte za sloj 2 (npr. interaktivni objekti)
                if (layer2[i, j] == ' ')
                {
                    // Možda ništa, samo prazno
                }

                // Postavi objekte za sloj 3 (npr. neprijatelji, specijalni objekti)
                if (layer3[i, j] == 'O')  // Na primer, 'O' predstavlja objekat na ovom sloju
                {
                    Instantiate(objectPrefab, position + new Vector3(0, 1, 0), Quaternion.identity);  // Postavlja objekat sa offsetom na Y
                }
            }
        }
    }
}
