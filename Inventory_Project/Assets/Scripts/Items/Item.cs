using UnityEngine;
using UnityEngine.Events;

public class Item : ScriptableObject
{
    /// <summary>
    /// In this example I'm using the asset created name to know the name of the object, but can be easily change everything to use this parameter.
    /// </summary>
    public string ItemName;

    /// <summary>
    /// This represents the item weight in kilograms.
    /// </summary>
    public float ItemWeight;

    /// <summary>
    /// This event is fired when the durability of an item reaches 0
    /// </summary>
    public UnityEvent<Item> MakeTrash = new UnityEvent<Item>();

    /// <summary>
    /// Dont set directly this variable, use Durability instead.
    /// </summary>
    public int durability = 1;

    /// <summary>
    /// Indicates the durability of the object, for the equipment this number go down when you attack with the weapon
    /// for the consumables and resources this goes down 1 every second until it's transformed in trash.
    /// </summary>
    public int Durability
    {
        get { return durability; }
        set {
            durability = value;
            if (durability <= 0)
                MakeTrash.Invoke(this);
        }
    }

    /// <summary>
    /// Description of the object.
    /// </summary>
    public string Description;

    /// <summary>
    /// Item image.
    /// </summary>
    public Sprite ItemImage;
}

//Some interfaces I use for the items.

public interface ISalable<T>
{
    public void Sell(T value);
}
public interface IEquipable
{
    public void Equip();
}

public interface IDegradable
{
    public void Degrad(float time);
}

public interface IUsable
{
    public void Use();
}