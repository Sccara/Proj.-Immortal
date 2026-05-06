using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private Resource _resource;

    private void OnEnable()
    {
        if (_resource != null)
            _resource.OnValueChanged += UpdateBar;
    }

    public void Init(Resource resource)
    {
        if (_resource != null)
        {
            _resource.OnValueChanged -= UpdateBar;
        }

        _resource = resource;

        if (_resource != null)
        {
            _resource.OnValueChanged += UpdateBar;
            UpdateBar(_resource.Percent);
        }
    }

    public void UpdateBar(float barPercent)
    {
        barImage.fillAmount = barPercent;
    }

    private void OnDisable()
    {
        if (_resource != null)
            _resource.OnValueChanged -= UpdateBar;
    }
}
