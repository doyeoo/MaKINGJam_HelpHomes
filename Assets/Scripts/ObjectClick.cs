using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public float moveObjectX;
    public float moveObjectY;
    public float moveSpeed;
    private GameObject target;
    //public Camera getCamera;
    private RaycastHit hit;

    Transform tran;


    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 velo = Vector2.zero;
        //Vector2 target = new Vector2(moveObjectX, moveObjectY);
        //tran.position = Vector2.SmoothDamp(tran.position, target, ref velo, moveSpeed);

        OnMouseDown();
    }

    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            if(target == this.gameObject)
            {
                Vector2 targetObj = new Vector2(moveObjectX, moveObjectY);

                tran.position = Vector2.Lerp(tran.position, targetObj, moveSpeed * Time.deltaTime);
            }
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if(Physics.Raycast(ray, out hit))
            //{
            //    //string objectName = hit.collider.gameObject.name;
            //    tran.position = Vector2.Lerp(tran.position, target, moveSpeed * Time.deltaTime);
            //}
            
            //tran.position = Vector2.Lerp(tran.position, target, moveSpeed * Time.deltaTime);
            //Vector2 velo = Vector2.zero;
            //Vector2 target = new Vector2(moveObjectX, moveObjectY);
            //tran.position = Vector2.SmoothDamp(tran.position, target, 0, moveSpeed);
        }

    }

    void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }
    }

}

