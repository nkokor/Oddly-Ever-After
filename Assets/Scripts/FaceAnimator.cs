using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public Texture texture1;  
    public Texture texture2;  
    private Renderer rend;
    private float timeElapsed = 0f;  
    private bool isTexture2Active = false;  

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 4f)
        {
            isTexture2Active = true;
            timeElapsed = 0f;  
        }

        if (isTexture2Active && timeElapsed >= 0.2f)
        {
            isTexture2Active = false;
            timeElapsed = 0f;  
        }

        if (isTexture2Active)
        {
            rend.material.mainTexture = texture2;
        }
        else
        {
            rend.material.mainTexture = texture1;
        }
    }
}
