using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{

    public float speed = 0f;
    public float maxDepth = 0f;

    public Transform KaraktärTransform;

    // Start is called before the first frame update
    void Start()
    {
        // I början av spelet blir lavans position samma som karaktärens på x-axelns och har ett avstånd på maxDepth under karktären
        Vector3 position = transform.position;
        position.y = KaraktärTransform.transform.position.y - maxDepth;
        position.x = KaraktärTransform.transform.position.x;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //Variablar för lavans max djup under karaktären
        float playerY = KaraktärTransform.transform.position.y;
        float maxLavaDepth = playerY - maxDepth;

        //Följer efter karaktären på x-axeln, likt så som kameran och backgrunden gör.
        Vector3 newPos = transform.position;
        newPos.x = KaraktärTransform.transform.position.x;
        transform.position = newPos;

        transform.position += Vector3.up * speed * Time.deltaTime;

        //Ifall lavans position skulle falla under max djupet som sattes så blir dens nya position samma på x-axelns men stannar kvar på maxDepth,
        //vilket gör att man inte kan hoppa undan lavan hur långt som helst
        if (transform.position.y < maxLavaDepth)
        {
            transform.position = new Vector3(transform.position.x, maxLavaDepth, transform.position.z);
        }
    }
}
