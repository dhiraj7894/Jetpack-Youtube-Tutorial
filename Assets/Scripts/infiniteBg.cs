using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteBg : MonoBehaviour
{
    public GameObject[] backgrounds;
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in backgrounds)
        {
            loadChildObject(obj);
        }
    }
    void loadChildObject(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childWant = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childWant; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
        Destroy(obj.GetComponent<PolygonCollider2D>());
    }

    void moveChildObject(GameObject obj)
    {
        Transform[] childs = obj.GetComponentsInChildren<Transform>();
        if (childs.Length > 1)
        {
            GameObject firstChild = childs[1].gameObject;
            GameObject lastChild = childs[childs.Length - 1].gameObject;
            float halfObjWith = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjWith)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjWith * 2, lastChild.transform.position.y, lastChild.transform.position.z);

            }else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjWith)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjWith * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
    private void LateUpdate()
    {
        foreach(GameObject obj in backgrounds)
        {
            moveChildObject(obj);
        }
    }

}
