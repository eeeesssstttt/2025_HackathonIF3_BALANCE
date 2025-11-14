using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Effect
{
    public int eco;
    public int pouvoir;
    public int sante;
    public int population;

    public Effect(int eco, int pouvoir, int sante, int population)
    {
        this.eco = eco;
        this.pouvoir = pouvoir;
        this.sante = sante;
        this.population = population;
    }
}

public class ScoreManager : MonoBehaviour
{
    // jauges
    [SerializeField] private Slider ecoSlider;
    [SerializeField] private Slider popuSlider;
    [SerializeField] private Slider santeSlider;
    [SerializeField] private Slider pouvoirSlider;

    // scene manager
    [SerializeField] private SceneLoader sceneLoader;

    private int MIN_VALUE = 0;
    private int MAX_VALUE = 30; // à revoir

    void Start()
    {
        ecoSlider.minValue = pouvoirSlider.minValue = santeSlider.minValue = popuSlider.minValue = MIN_VALUE;
        ecoSlider.maxValue = pouvoirSlider.maxValue = santeSlider.maxValue = popuSlider.maxValue = MAX_VALUE;
        ecoSlider.value = pouvoirSlider.value = santeSlider.value = popuSlider.value = 15;
    }

    public void SliderEffect(Effect effect)
    {
        ecoSlider.value = Mathf.Clamp(ecoSlider.value + effect.eco, MIN_VALUE, MAX_VALUE);
        pouvoirSlider.value = Mathf.Clamp(pouvoirSlider.value + effect.pouvoir, MIN_VALUE, MAX_VALUE);
        santeSlider.value = Mathf.Clamp(santeSlider.value + effect.sante, MIN_VALUE, MAX_VALUE);
        popuSlider.value = Mathf.Clamp(popuSlider.value + effect.population, MIN_VALUE, MAX_VALUE);

        CheckEnd();
    }

    void CheckEnd()
    {
        if (ecoSlider.value <= MIN_VALUE || pouvoirSlider.value <= MIN_VALUE || popuSlider.value <= MIN_VALUE || santeSlider.value <= MIN_VALUE)
        {
            sceneLoader.ChangeScene("End_min");
            return;
        }
        if (ecoSlider.value >= MAX_VALUE || pouvoirSlider.value >= MAX_VALUE || popuSlider.value >= MAX_VALUE || santeSlider.value >= MAX_VALUE)
        {
            sceneLoader.ChangeScene("End_max");
            return;
        }
        else
        {
            sceneLoader.ChangeScene("End_balance");
        }
    }
}
