class Sborka
{
    List<ArtProperty> SelfResistance = new List<ArtProperty>(){
        new ArtProperty("Радиация","0,5"),new ArtProperty("Температура","0,5"),
        new ArtProperty("Биологическое заражение","0,5"),
        new ArtProperty("Пси-излучение","1,5"),
        new ArtProperty("Холод","1")};
    public List<string> ArtefactNames = new List<string>();
    public List<ArtProperty> Properties = new List<ArtProperty>();
    
    public Sborka(List<Artefact> Artefacts){
        foreach (var artefact in Artefacts)
        {
            ArtefactNames.Add(artefact.Name);
        }
        foreach (var artefact in Artefacts)
        {
            foreach (var prop in artefact.Properties)
            {
                Properties.Add(prop);
            }
        }
    }

    public bool ContainProperty(ArtProperty prop){
        foreach (var property in Properties)
        {
            if(property == prop){
                return true;
            }
        }
        return false;
    }

    public ArtProperty GetPropertyByName(string PropertyName){
        foreach (var property in Properties)
        {
            if(PropertyName == property.Name){
                return property;
            }
        }
        return null;
    }

    public bool CalculateSborka(int resistance){
        List<ArtProperty> CashArtProp = new List<ArtProperty>();
        for (int i = 0; i < Properties.Count; i++)
        {
            ArtProperty pr = new ArtProperty(Properties[i].Name,Properties[i].Value);
            if(CashArtProp.Count == 0)
            {
                for (int j = i+1; j < Properties.Count; j++)
                {
                    if(pr.Name == Properties[j].Name){
                        if (pr.Value.Contains("%"))
                        {
                            pr.Value = double.Round(Convert.ToDouble(pr.Value.Substring(0,pr.Value.Length - 1)) + Convert.ToDouble(Properties[j].Value.Substring(0,Properties[j].Value.Length - 1)),2,MidpointRounding.ToEven).ToString()+"%";  
                        }else{                  
                            pr.Value = double.Round(Convert.ToDouble(pr.Value) + Convert.ToDouble(Properties[j].Value),2,MidpointRounding.ToEven).ToString();
                        }
                    }
                }
                CashArtProp.Add(pr);
            }else
            {
                List<string> Artnames = new List<string>();
                foreach (var cahsartprop in CashArtProp.ToList())
                {
                    Artnames.Add(cahsartprop.Name);
                }
                if(!Artnames.Contains(pr.Name)){
                    for (int j = i+1; j < Properties.Count; j++)
                    {
                        if(pr.Name == Properties[j].Name){
                            if (pr.Value.Contains("%"))
                            {
                                pr.Value = double.Round(Convert.ToDouble(pr.Value.Substring(0,pr.Value.Length - 1)) + Convert.ToDouble(Properties[j].Value.Substring(0,Properties[j].Value.Length - 1)),2,MidpointRounding.ToEven).ToString()+"%";  
                            }else{                  
                                pr.Value = double.Round(Convert.ToDouble(pr.Value) + Convert.ToDouble(Properties[j].Value),2,MidpointRounding.ToEven).ToString();
                            }
                        }
                    }
                    CashArtProp.Add(pr);
                }
            }
        }
       
        foreach (var proper in CashArtProp)
        {
            foreach (var selfprop in SelfResistance)
            {
                if(proper.Name ==  selfprop.Name && proper.Name != "Холод"){
                    proper.Value = (Convert.ToDouble(proper.Value)/100*(100-resistance)).ToString();
                    if((Convert.ToDouble(selfprop.Value)- Convert.ToDouble(proper.Value)) < -0.1){
                        return false;
                    }
                }if (proper.Name == "Холод" ){
                    if((Convert.ToDouble(selfprop.Value)- Convert.ToDouble(proper.Value)) < -0.1){
                        return false;
                    }
                }
            }
        }
        Properties = new List<ArtProperty>(CashArtProp);
        return true;
    }

    public Sborka CompareSborkas(Sborka sborka1,Sborka sborka2,List<string> PropertysToCompare,int resistance ){
        List<ArtProperty> valuestocompare = new List<ArtProperty>();
        string delta = "3";
        ArtProperty prop1 = new ArtProperty("Cash","");
        ArtProperty prop2 = new ArtProperty("Cash","");
        bool res1 = sborka1.CalculateSborka(resistance);
        bool res2 = sborka2.CalculateSborka(resistance);
        if(res1 && res2){ // Если обе сборки - играбельны ищем лучшую
            foreach (var propertytocompare in PropertysToCompare)
            {
                foreach (var property1 in sborka1.Properties) // Находим свойство у 1 сборки
                {
                    if(property1.Name == propertytocompare){
                        prop1 = property1;
                    }
                }
                foreach (var property2 in sborka2.Properties) // Находим свойство у 2 сборки
                {
                    if(property2.Name == propertytocompare){
                        prop2 = property2;
                    }
                }

                if (PropertysToCompare.Count == 1){ //Если у нас только 1 параметр для сравнения 
                    if(prop1.Value != "" && prop2.Value != ""){ // Если оба свойства != 0
                        if (prop1.Value == prop2.Value)
                        {
                            return sborka1;
                        }
                        if (prop1.Value.Contains("%"))
                        {
                            if (Convert.ToDouble(prop1.Value.Substring(0,prop1.Value.Length - 1)) > Convert.ToDouble(prop2.Value.Substring(0,prop2.Value.Length - 1))){
                                return sborka1;
                            }else return sborka2;  
                        }else{      
                            if (Convert.ToDouble(prop1.Value) > Convert.ToDouble(prop2.Value))
                            {
                                return sborka1;
                            }else return sborka2;
                        }
                    }else if (prop2.Value != "" ){
                        return sborka2;
                    }else if (prop1.Value != "" ){
                        return sborka1;
                    }else return sborka1;
                }else { //Если у нас только несколько параметров для сравнения 
                    if(prop1.Value != "" && prop2.Value != ""){ // Если оба свойства != 0
                        if (prop1.Value == prop2.Value)
                        {
                            valuestocompare.Add(prop1);
                        }
                        if (prop1.Value.Contains("%"))
                        {
                            if (Convert.ToDouble(prop1.Value.Substring(0,prop1.Value.Length - 1)) > Convert.ToDouble(prop2.Value.Substring(0,prop2.Value.Length - 1))){
                                valuestocompare.Add(prop1);
                            }else valuestocompare.Add(prop2);  
                        }else{      
                            if (Convert.ToDouble(prop1.Value) > Convert.ToDouble(prop2.Value))
                            {
                                valuestocompare.Add(prop1);
                            }else valuestocompare.Add(prop2);
                        }
                    }else if (prop2.Value != "" ){
                        valuestocompare.Add(prop2);
                    }else if (prop1.Value != "" ){
                        valuestocompare.Add(prop1);
                    }else valuestocompare.Add(prop1);
                }   
            }

            if(sborka1.ContainProperty(valuestocompare[0]) && sborka1.ContainProperty(valuestocompare[1])){
                return sborka1;
            }else if (sborka2.ContainProperty(valuestocompare[0]) && sborka2.ContainProperty(valuestocompare[1])){
                return sborka2;
            }else{
                return sborka1;
            }
        }else if (res1){ // Если 1 сборка - играбельна
            return sborka1;
        }else if (res2){ // Если 2 сборка - играбельна
            return sborka2;
        }else return sborka1;  // Если обе неиградельны - возвращаем первую
    }

}