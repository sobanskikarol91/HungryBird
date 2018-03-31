using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Color blue;
    public Color gold;
    public Color green;
    public Color pink;
    public Color red;
    public Color brown;

    public Animator _anim;
    public Text damageText;

    float small = 0.6f;

    void Start()
    {
        if (_anim == null) Debug.LogError("There is no Animator in FlaotingText ");
        AnimatorClipInfo[] clipInfo = _anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }


    public void TextAppearance(int score)
    {
        if (score < 50)
            ScaleAndColor(Color.white, 0.8f);
        if (score >= 50 && score < 100)
            ScaleAndColor(blue, 1.15f);
        else if (score >= 100 && score < 200)
            ScaleAndColor(pink, 1.25f);
        else if (score >= 200)
            ScaleAndColor(gold, 1.37f);

        damageText.text = "+" + score;
    }

    public void TextAppearance(int value, EPICKUP type)
    {
        if (type == EPICKUP.Ammo)
            ScaleAndColor(brown, small, "+", value, "Eggs");
        else if (type == EPICKUP.cash)
            ScaleAndColor(gold, 1.3f, "+", value, "$");
        else if (type == EPICKUP.Consumable)
            ScaleAndColor(green, small, "-", value, "Hunger");
        else if (type == EPICKUP.ProtectedShield || type == EPICKUP.DamageShield)
            TextAppearance(type);
    }

    public void TextAppearance(EPICKUP type)
    {
        if (type == EPICKUP.ProtectedShield)
            ScaleAndColor(blue, small, "Protecion Shield");
        else if (type == EPICKUP.DamageShield)
            ScaleAndColor(red, small, "Damage Shield");
    }

    void ScaleAndColor(Color color, float scale)
    {
        damageText.color = color;
        transform.localScale *= scale;
    }

    void ScaleAndColor(Color color, float scale, string sign, int value, string label)
    {
        damageText.color = color;
        damageText.text = sign + value + " " + label;
        transform.localScale *= scale;
    }

    void ScaleAndColor(Color color, float scale, string label)
    {
        damageText.color = color;
        damageText.text = label;
        transform.localScale *= scale;
    }
}
