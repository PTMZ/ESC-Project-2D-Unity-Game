using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButton : MonoBehaviour
{
    //public ButtonTrail buttonTrail;
    public Sprite normSprite;
    public Sprite pressSprite;
    public GameObject trailLock;
    public GameObject trailSignal;
    public GameObject tick;
    public GameObject cross;
    public GameObject laser;
    public int colour;
    private int count = 0;
    private SpriteRenderer mySpriteRenderer;

    private GameObject objRef = null;

    void Start(){
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = normSprite;
        setTrailState(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("DestroyableBox")){
            if(other.gameObject.GetComponent<Destroyable>().curSprite == colour){
                objRef = other.gameObject;
                count ++;
            }
        }
        if(getPressed()){
            mySpriteRenderer.sprite = pressSprite;
            setTrailState(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("DestroyableBox")){
            if(other.gameObject.GetComponent<Destroyable>().curSprite == colour){
                count --;
            }
        }
        if(!getPressed()){
            mySpriteRenderer.sprite = normSprite;
            setTrailState(false);
        }
    }

    void FixedUpdate(){
        if(objRef == null && getPressed()){
            count --;
        }
        if(!getPressed()){
            mySpriteRenderer.sprite = normSprite;
            setTrailState(false);
        }
    }

    public bool getPressed(){
        return count>0;
    }

    private void setTrailState(bool state){
        if(trailLock){
            trailLock.SetActive(!state);
            trailSignal.SetActive(state);
        }
        if(cross){
            cross.SetActive(!state);
            tick.SetActive(state);
        }
        laser.SetActive(!state);
    }
}
