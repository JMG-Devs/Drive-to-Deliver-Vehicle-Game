using UnityEngine;
using DG.Tweening;

public class MessageScript : MonoBehaviour
{
    public SpriteRenderer sp;
    public bool IsStarting;
    public GameObject sparkParticle;
    private void OnEnable()
    {
        if (IsStarting)
        {
            transform.DOScale(new Vector3(0, 1, 1), 0.5f).From().SetDelay(1.25f).SetEase(Ease.OutBack);
            sp.DOFade(0, 0.5f).SetDelay(2.5f).SetEase(Ease.InOutSine);
            transform.DOLocalMove(new Vector3(0, 0.5f, 0), 0.5f).SetRelative().SetDelay(2.5f).SetEase(Ease.InOutSine).OnComplete(
                () => gameObject.SetActive(false)); ;
        
        }
        else
        {
            transform.DOScale(new Vector3(0, 1, 1), 0.5f).SetDelay(.75f).From().SetEase(Ease.OutBack);
            sp.DOFade(0, 0.5f).SetDelay(2f).SetEase(Ease.InOutSine);
            transform.DOLocalMove(new Vector3(0, 0.5f, 0), 0.5f).SetRelative().SetDelay(2f).SetEase(Ease.InOutSine).OnComplete(
                () => gameObject.SetActive(false)); ;
       
        }
    }
}
