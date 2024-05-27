using UnityEngine;
using UnityEngine.UI;

public class ResourceInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private Item item;

    [SerializeField] private int itemCount;

    [SerializeField] private float interactDistanceThreshold;

    [SerializeField] private int minInteracts;

    [SerializeField] private int maxInteracts;

    [SerializeField] private GameObject interactCanvas;

    [SerializeField] private Slider interactSlider;

    private int _randomInteracts;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    private void Awake()
    {
        _randomInteracts = Random.Range(minInteracts, maxInteracts);
        interactSlider.maxValue = _randomInteracts;
    }

    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            _randomInteracts--;

            interactCanvas.SetActive(true);
            interactSlider.value = _randomInteracts;

            if (_randomInteracts <= 0)
            {
                for (int i = 0; i < itemCount; i++)
                {
                    GameManager.ResourceManager.SpawnResource(item, this.transform.position);
                }

                interactCanvas.SetActive(false);

                Destroy(this.gameObject);
            }
        }
    }
}

public interface Interactive
{
    public float InteractDistanceThreshold { get; }

    public void Interact();
}
