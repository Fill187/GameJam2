using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentoOggetti : MonoBehaviour
{

    float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position=transform.position - Vector3.forward * Time.deltaTime * speed;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
