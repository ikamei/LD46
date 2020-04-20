using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickRestart : MonoBehaviour
{
    public Image image;
    public GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        image.DOFade(1f, 1f).From(0).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameController.SetState(GameController.STATE_START);
        }
    }
}
