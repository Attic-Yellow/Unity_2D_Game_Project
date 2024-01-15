

[System.Serializable]
public class SnowManData
{
    public string snowManName;
    public string snowManDescription;
    public string snowManspecialEffect;
    public int price;

    public SnowManData(string snowManName, string snowManDescription, string snowManspecialEffect, int price)
    {
        this.snowManName = snowManName;
        this.snowManDescription = snowManDescription;
        this.snowManspecialEffect = snowManspecialEffect;
        this.price = price;
    }
}
