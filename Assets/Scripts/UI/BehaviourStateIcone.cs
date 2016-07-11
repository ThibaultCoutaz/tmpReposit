using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BehaviourStateIcone : MonoBehaviour {

    public int ID;
    public Image FiledImage;
    public float CoolDown;

    private float currentCD=0;
    private bool init = true;
    public bool End = false;

	// Use this for initialization
	void Start () {
        FiledImage.fillAmount = 1;
    }
	

	// Update is called once per frame
	void Update () {
        if (CoolDown > 0)
        {
            currentCD += Time.deltaTime;
            FiledImage.fillAmount = 1 - currentCD / CoolDown;
            if (currentCD >= CoolDown)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (init)
            {
                FiledImage.fillAmount = 0;
                init = false;
            }
            else if (End)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
