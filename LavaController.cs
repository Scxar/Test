using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{

    public float speed = 0f;
    public float maxDepth = 0f;

    public Transform Karakt�rTransform;

    // Start is called before the first frame update
    void Start()
    {
        // I b�rjan av spelet blir lavans position samma som karakt�rens p� x-axelns och har ett avst�nd p� maxDepth under karkt�ren
        Vector3 position = transform.position;
        position.y = Karakt�rTransform.transform.position.y - maxDepth;
        position.x = Karakt�rTransform.transform.position.x;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //Variablar f�r lavans max djup under karakt�ren
        float playerY = Karakt�rTransform.transform.position.y;
        float maxLavaDepth = playerY - maxDepth;

        //F�ljer efter karakt�ren p� x-axeln, likt s� som kameran och backgrunden g�r.
        Vector3 newPos = transform.position;
        newPos.x = Karakt�rTransform.transform.position.x;
        transform.position = newPos;

        transform.position += Vector3.up * speed * Time.deltaTime;

        //Ifall lavans position skulle falla under max djupet som sattes s� blir dens nya position samma p� x-axelns men stannar kvar p� maxDepth,
        //vilket g�r att man inte kan hoppa undan lavan hur l�ngt som helst
        if (transform.position.y < maxLavaDepth)
        {
            transform.position = new Vector3(transform.position.x, maxLavaDepth, transform.position.z);
        }
    }
}
