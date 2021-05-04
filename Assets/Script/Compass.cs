using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject baliseIcon;
    public RawImage compassImage;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        AddMarker();
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }

    public void AddMarker()//GameObject balise)
    {
        GameObject marker = Instantiate(baliseIcon, compassImage.transform);
    }
}
