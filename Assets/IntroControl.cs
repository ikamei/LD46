using UnityEngine;
using UnityEngine.UI;

public class IntroControl : MonoBehaviour
{
    public GameController gameController;
    public PageControl[] startPage;
    public Image[] images;

    int index = 0;
    
    void Start()
    {
        StartIntro();
    }

    public void StartIntro()
    {
        images[index].gameObject.SetActive(true);
        startPage[index].gameObject.SetActive(true);
    }

    public void Next()
    {
        images[index].gameObject.SetActive(false);
        index++;
        if (index >= images.Length)
        {
            Done();
            return;
        }
        images[index].gameObject.SetActive(true);
        startPage[index].gameObject.SetActive(true);
    }

    void Done()
    {
        gameController.SetState(GameController.STATE_GAMING);
    }
}
