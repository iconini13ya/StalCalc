
class Artefact
{   
    public string Name {get;set;}
    public List<ArtProperty> Properties = new List<ArtProperty>();

    public Artefact(string name, List<ArtProperty> properties){
        Name = name;
        Properties = properties;
    }
}