using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject Corner1;
    public GameObject Corner2;
    public GameObject Corner3;
    public GameObject Fence1;
    public GameObject Fence2;
    public GameObject Fence3;
    
    private char[,] ground = {
        { 'A', 'D', 'D', 'E', 'D', 'D', 'F', 'F', 'E', 'D', 'D', 'D', 'B' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'F', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'E', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'F', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'D', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'D' },
        { 'C', 'D', 'F', 'D', 'E', 'F', 'O', 'D', 'F', 'E', 'D', 'D', 'A' },
    };



    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        Renderer grassGroundRenderer = Fence1.GetComponent<Renderer>();
        float tileSize = grassGroundRenderer.bounds.size.x; 

        for (int i = 0; i < ground.GetLength(0); i++)
        {
            for (int j = 0; j < ground.GetLength(1); j++)
            {
                Vector3 position = new Vector3(j * tileSize, 0, i * tileSize); 

                switch (ground[i, j])
                {
                    case 'A':
                        Instantiate(Corner1, position, Quaternion.identity);
                        break;
                    case 'B':
                        Instantiate(Corner2, position, Quaternion.identity);
                        break;
                    case 'C':
                        Instantiate(Corner3, position, Quaternion.identity);
                        break;
                    case 'D':
                        Instantiate(Fence1, position, Quaternion.identity);
                        break;
                    case 'E':
                        Instantiate(Fence2, position, Quaternion.identity);
                        break;
                    case 'F':
                        Instantiate(Fence3, position, Quaternion.identity);
                        break;
                }

            }
        }
    }
}
