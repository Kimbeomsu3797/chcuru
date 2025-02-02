using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage;
    public GameObject itemPrefabs;

    public bool isClosed = true;

    public int arrangeId = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isClosed && collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false;
            if(itemPrefabs != null)
            {
                Instantiate(itemPrefabs, transform.position, Quaternion.identity);

            }
            //��ġ Id ���
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
