using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButtonBlue : MonoBehaviour
{
    //public ButtonTrail buttonTrail;
    public Sprite normSprite;
    public Sprite pressSprite;
    public GameObject trailLock;
    public GameObject trailSignal;
    public GameObject tick;
    public GameObject cross;
    public GameObject laser;
    private int count = 0;
    private SpriteRenderer mySpriteRenderer;

    void Start(){
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = normSprite;
        trailLock.SetActive(true);
        trailSignal.SetActive(false);
        cross.SetActive(true);
        tick.SetActive(false);
        laser.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("DestroyableBoxBlue")){
            count ++;
        }
        if(getPressed()){
            mySpriteRenderer.sprite = pressSprite;
            trailLock.SetActive(false);
            trailSignal.SetActive(true);
            cross.SetActive(false);
            tick.SetActive(true);
            laser.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("DestroyableBoxBlue")){
            count --;
        }
        if(!getPressed()){
            mySpriteRenderer.sprite = normSprite;
            trailLock.SetActive(true);
            trailSignal.SetActive(false);
            cross.SetActive(true);
            tick.SetActive(false);
            laser.SetActive(true);
        }
    }

    public bool getPressed(){
        return count>0;
    }
}
