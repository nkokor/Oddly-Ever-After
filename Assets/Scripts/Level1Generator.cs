using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject Corner1;
    public GameObject Corner2;
    public GameObject Corner3;
    public GameObject Corner4;
    public GameObject Edge1;
    public GameObject Edge2;
    public GameObject Edge3;
    public GameObject Edge4;
    public GameObject Grass1;
    public GameObject Grass2;
    public GameObject Grass3;
    public GameObject Grass4;
    public GameObject StraightPath;
    public GameObject CrossPath;
    public GameObject TripleCrossPath;
    public GameObject TurnPath;


    
    private char[,] ground = {
        { 'A', 'E', 'E', 'F', 'E', 'E', 'G', 'H', 'F', 'E', 'E', 'E', 'B' },
        { 'E', 'O', 'O', 'O', 'Q', 'P', 'Q', 'O', 'O', 'Q', 'O', 'O', 'E' },
        { 'E', 'O', 'O', 'P', 'O', 'O', 'O', 'P', 'Q', 'O', 'O', 'O', 'E' },
        { 'E', 'O', 'O', 'R', 'O', 'O', 'O', 'O', 'O', 'P', 'O', 'P', 'E' },
        { 'G', 'O', 'O', 'O', 'R', 'O', 'O', 'O', 'O', 'O', 'Q', 'Q', 'E' },
        { 'F', 'O', 'Q', 'O', 'O', 'O', 'Q', 'O', 'O', 'O', 'O', 'O', 'E' },
        { 'E', 'P', 'O', 'P', 'O', 'R', 'O', 'O', 'R', 'P', 'O', 'O', 'E' },
        { 'H', 'O', 'O', 'O', 'O', 'O', 'P', 'O', 'O', 'O', 'Q', 'O', 'E' },
        { 'E', 'O', 'O', 'O', 'O', 'O', 'O', 'R', 'O', 'O', 'O', 'Q', 'E' },
        { 'E', 'Q', 'O', 'R', 'O', 'O', 'O', 'O', 'O', 'O', 'R', 'P', 'E' },
        { 'C', 'E', 'G', 'E', 'F', 'H', 'O', 'E', 'G', 'F', 'E', 'E', 'D' },
    };



    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        Renderer grassGroundRenderer = Grass1.GetComponent<Renderer>();
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
                        Instantiate(Corner4, position, Quaternion.identity);
                        break;
                    case 'E':
                        Instantiate(Edge1, position, Quaternion.identity);
                        break;
                    case 'F':
                        Instantiate(Edge2, position, Quaternion.identity);
                        break;
                    case 'G':
                        Instantiate(Edge3, position, Quaternion.identity);
                        break;
                    case 'H':
                        Instantiate(Edge3, position, Quaternion.identity);
                        break;
                    case 'O':
                        Instantiate(Grass1, position, Quaternion.identity);
                        break;
                    case 'P':
                        Instantiate(Grass2, position, Quaternion.identity);
                        break;
                    case 'Q':
                        Instantiate(Grass3, position, Quaternion.identity);
                        break;
                    case 'R':
                        Instantiate(Grass4, position, Quaternion.identity);
                        break;
                    case 'I':
                        Instantiate(StraightPath, position, Quaternion.identity);
                        break;
                    case 'J':
                        Instantiate(TripleCrossPath, position, Quaternion.identity);
                        break;
                    case 'K':
                        Instantiate(TurnPath, position, Quaternion.identity);
                        break;
                    case 'L':
                        Instantiate(CrossPath, position, Quaternion.identity);
                        break;
                }

            }
        }
    }
}
