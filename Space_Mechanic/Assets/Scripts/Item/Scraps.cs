using UnityEngine;

public class Scraps : Item
{
    public Scraps(int itemCount) : base(itemCount)
    {

    }

    public Scraps()
    {
        
    }

    public override void PickUp()
    {
        FindAnyObjectByType<ScrapsCounter>().AddScraps(itemCount);
        base.PickUp();
    }
}
