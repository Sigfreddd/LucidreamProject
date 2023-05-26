using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : Road
{
    public float alertTime;

    [SerializeField]
    private SpriteRenderer spriteSignal;

    public Sprite notAlertSprite;
    public Sprite alertSprite;

    protected override IEnumerator LaunchTrains()
    {
        yield return new WaitForSeconds(1f);

        spriteSignal.sprite = alertSprite;

        yield return new WaitForSeconds(alertTime);

        spriteSignal.sprite = notAlertSprite;
}
}
