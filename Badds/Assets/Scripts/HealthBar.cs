using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Ссылка на компонент Slider для управления шкалой здоровья
    public Gradient gradient; // Градиент для изменения цвета шкалы здоровья
    public Image fill; // Изображение заполнения шкалы здоровья

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + new Vector3(0, 1.5f, 0); // Устанавливаем позицию шкалы здоровья над врагом
    }
}
