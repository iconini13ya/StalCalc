using AngleSharp.Text;
using CodeBeautify;
internal class Program
{
    private static async Task Main(string[] args)
    {
        string[] BlockList = new string[] {"Многогранник"};
        var Artefacts = new List<Artefact>();
        string? Artname = null;
        string? Propname = null;
        string? Value = null;
        var Prop = new List<ArtProperty>();

        //========================================================
        var cursor = Directory.GetDirectories(@"E:\StalArtCalc\artefact");
        List<string> ArtTypeDer = cursor.ToList();
        foreach (var arttypeder in ArtTypeDer)
        {
            foreach (var artfile in Directory.GetFiles(arttypeder))
            {
                var jsonstring = File.ReadAllText(artfile);
                var Artefact = Welcome10.FromJson(jsonstring);
                if (!BlockList.Contains(Artefact.Name.Lines.Ru)){
                    Artname = Artefact.Name.Lines.Ru;
                    // Console.WriteLine(Artname);
                    Prop = new List<ArtProperty>();
                    foreach (var Propertyblock in Artefact.InfoBlocks)
                    {
                        if (Propertyblock is not null && Propertyblock.Elements is not null)
                        {
                            foreach (var Element in Propertyblock.Elements)
                            {
                                if(Element is not null && Element.Name is not null && Element.Name.Lines is not null && Element.Name.Lines.Ru is not null && Element.Min is not null && Element.Max is not null){
                                    Propname = Element.Name.Lines.Ru;
                                        if (Element.Formatted.Value.Ru.Contains("+"))
                                        {
                                            Value = Element.Formatted.Value.Ru.Substring(Element.Formatted.Value.Ru.IndexOf(";")+1,Element.Formatted.Value.Ru.IndexOf("]")-Element.Formatted.Value.Ru.IndexOf(";")-1).Replace(" ","").Replace(".",",");
                                        }else Value = Element.Formatted.Value.Ru.Substring(Element.Formatted.Value.Ru.IndexOf("[")+1,Element.Formatted.Value.Ru.IndexOf(";")-Element.Formatted.Value.Ru.IndexOf("[")-1).Replace(".",",");
                                    Prop.Add(new ArtProperty(Propname,Value));
                                }
                            }
                        }   
                    }  
                    Artefacts.Add(new Artefact(Artname, Prop));                 
                }
                
                
            }
        }
        var fialar = new List<Artefact>(){Artefacts[57],Artefacts[57],Artefacts[57],Artefacts[57]};
        Sborka finalSborka = new Sborka(fialar);
        //======================
        int CountOfArtefacts = 0;
        string NeedToFindProperty = "";
        int Resistance = -1;
        while ( CountOfArtefacts == 0)
        {
            Console.WriteLine("Введите число артефактов от 1 до 4");
            try
            {
                CountOfArtefacts = Convert.ToInt32(Console.ReadLine());
                if (CountOfArtefacts < 0 || CountOfArtefacts > 4){
                CountOfArtefacts = 0;  
                }
            }
            catch (System.Exception)
            {
              Console.WriteLine("Вы попытались ввести что-то отличное от числа от 1 до 6");
              CountOfArtefacts = 0;
            }
            if (NeedToFindProperty.Equals("") && CountOfArtefacts != 0){
                while (NeedToFindProperty.Equals("")){
                    Console.WriteLine("Введите свойство которое нужно вычислить");
                    NeedToFindProperty = Console.ReadLine();
                    List<string> PropretiesCash= new List<string>();
                    foreach (var artefactcash in Artefacts)
                        {
                            foreach (var properycash in artefactcash.Properties)
                            {
                                if(!PropretiesCash.Contains(properycash.Name)){
                                    PropretiesCash.Add(properycash.Name.ToLower());
                                }
                            }
                        }
                        if (!PropretiesCash.Contains(NeedToFindProperty.ToLower())){
                            Console.WriteLine("Такого свойства нет, попробуйте другое свойство");
                            NeedToFindProperty = "";
                        }
                }
            }
            if (Resistance <=0 && !NeedToFindProperty.Equals("") && CountOfArtefacts != 0){
                while (Resistance <=0)
                {
                    Console.WriteLine("Введите сопротивление контейнера от 0 до 100");
                    try
                    {
                        Resistance = Convert.ToInt32(Console.ReadLine());
                        if (Resistance < 0 || Resistance > 100){
                        Resistance = -1;  
                        }
                    }
                    catch (System.Exception)
                    {
                    Console.WriteLine("Вы попытались ввести что-то отличное от числа от 0 до 100");
                    Resistance = -1;
                    }
                }
            }
        }

        switch (CountOfArtefacts)
        {
            case 1:
            foreach (var artefact1 in Artefacts)
            {
                Console.WriteLine("Сейчас в обработке артефакт "+ artefact1.Name);
                var ar = new List<Artefact>(){artefact1};
                var cashsborka = new Sborka(ar);
                finalSborka = cashsborka.CompareSborkas(finalSborka,cashsborka,new List<string>{NeedToFindProperty},Resistance); //Переносимый вес // Эффективность лечения //       
            }
            break;
            case 2:
            foreach (var artefact1 in Artefacts)
            {
                Console.WriteLine("Сейчас в обработке артефакт "+ artefact1.Name);
                foreach (var artefact2 in Artefacts)
                {
                var ar = new List<Artefact>(){artefact1,artefact2};
                var cashsborka = new Sborka(ar);
                finalSborka = cashsborka.CompareSborkas(finalSborka,cashsborka,new List<string>{NeedToFindProperty},Resistance); //Переносимый вес // Эффективность лечения //       
                }
            }
            break;
            case 3:
            foreach (var artefact1 in Artefacts)
            {
                Console.WriteLine("Сейчас в обработке артефакт "+ artefact1.Name);
                foreach (var artefact2 in Artefacts)
                {
                    foreach (var artefact3 in Artefacts)
                    {
                    var ar = new List<Artefact>(){artefact1,artefact2,artefact3};
                    var cashsborka = new Sborka(ar);
                    finalSborka = cashsborka.CompareSborkas(finalSborka,cashsborka,new List<string>{NeedToFindProperty},Resistance); //Переносимый вес // Эффективность лечения //      
                    }
                }
            }
            break;
            case 4:
            foreach (var artefact1 in Artefacts)
            {
                Console.WriteLine("Сейчас в обработке артефакт "+ artefact1.Name);
                foreach (var artefact2 in Artefacts)
                {
                    foreach (var artefact3 in Artefacts)
                    {
                        foreach (var artefact4 in Artefacts)
                        {
                            var ar = new List<Artefact>(){artefact1,artefact2,artefact3,artefact4};
                            var cashsborka = new Sborka(ar);
                            finalSborka = cashsborka.CompareSborkas(finalSborka,cashsborka,new List<string>{NeedToFindProperty},50); //Переносимый вес // Эффективность лечения //      
                        }
                    }
                }
            }
            break;
            default:break;
        }
        Console.WriteLine("Ваша сборка:");
        foreach (var art in finalSborka.ArtefactNames)
        {
            Console.WriteLine(art);
        }
        foreach (var pr in finalSborka.Properties)
        {
            Console.WriteLine(pr.Name);
            Console.WriteLine(pr.Value);
        }
    }
}