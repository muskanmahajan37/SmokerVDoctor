using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Progress : MonoBehaviour
{

    public float waitTime = 0.0f;
    public float MAXFILL = 100;
    private float currentFill;
    Image foregroundImage; 

    void Start()
    {
        foregroundImage = gameObject.GetComponent<Image>();
        this.currentFill = MAXFILL;
    }

    private void Update()
    {
        //foregroundImage.fillAmount -= (1f / 100.0f) / waitTime * Time.deltaTime;
    }

    public void reduceFill(float amount) {
        foregroundImage.fillAmount -= amount / MAXFILL;
        currentFill -= amount / MAXFILL;
        currentFill = Mathf.Max(0, currentFill);
    }

    public void addFill(float amount) {
        //foregroundImage.fillAmount += amount / MAXFILL;
        //currentFill += amount / MAXFILL;
        //currentFill = Mathf.Min(MAXFILL, currentFill);
    }

    public float getCurrentFill() {
        return this.currentFill;
    }

    public float getMaxFill() {
        return this.MAXFILL;
    }
}