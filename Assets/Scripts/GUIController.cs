using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    [SerializeField] Slider probability;
    [SerializeField] Slider delay;

    [SerializeField] Text probabilityValue;
    [SerializeField] Text delayValue;

    [SerializeField] Grid grid;


    private void Start()
    {
        probability.value = grid.edgeProbability;
        delay.value = grid.generationStepDelay;

        probabilityValue.text = probability.value.ToString();
        delayValue.text = delay.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        probabilityValue.text = probability.value.ToString();
        delayValue.text = delay.value.ToString();

        grid.generationStepDelay = delay.value;
        grid.edgeProbability = probability.value;
    }
}
